using System;
using System.Linq;
using System.Management.Automation;
using Elasticsearch.Net;
using Nest;

namespace Elasticsearch.Powershell
{
    public class ElasticCmdlet : Cmdlet
    {
        private ElasticClient _client;
        private IConnectionSettingsValues _connectionSettings;

        public ElasticCmdlet()
        {
            this.Node = new[] { "http://localhost:9200" };
        }

        [Parameter(Position = 0, Mandatory = false, HelpMessage = "The cluster node(s) urls (ex. http://localhost:9200)")]
        public string[] Node { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Credentials used for authentication")]
        public PSCredential Credential { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Authentication mode, only basic supported at the moment")]
        [ValidateSet("Basic", IgnoreCase = true)]
        public string AuthenticationMode { get; set; } = "Basic";

        [Parameter(Mandatory = false, HelpMessage = "Path to client certificate used to authenticate all HTTP requests")]
        public string CertificatePath { get; set; }

        [Parameter(HelpMessage = "Specify this switch to disable ssl certificate validation")]
        public SwitchParameter DisableSslCertificateValidation { get; set; }

        protected IElasticClient Client
        {
            get
            {
                return _client ?? (_client = new ElasticClient(this.ConnectionSettings));
            }
        }

        protected IConnectionSettingsValues ConnectionSettings
        {
            get
            {
                return _connectionSettings ?? (_connectionSettings = CreateConnectionSettings());
            }
            set
            {
                _connectionSettings = value;
            }
        }

        private ConnectionSettings CreateConnectionSettings()
        {
            var pool = new StaticConnectionPool(this.Node.Select(GetNodeUri));
            var ret = new ConnectionSettings(pool);

            if(this.Credential != null)
            {
                switch(this.AuthenticationMode.ToLower())
                {
                    case "basic":
                    default:
                        WriteVerbose($"Using basic authentication\n");
                        ret.BasicAuthentication(this.Credential.UserName, this.Credential.GetNetworkCredential().Password);
                        break;
                }
            }

            if (!String.IsNullOrWhiteSpace(this.CertificatePath))
            {
                WriteVerbose($"Using client certificate {this.CertificatePath}\n");
                ret.ClientCertificate(this.CertificatePath);
            }

            if(this.DisableSslCertificateValidation.IsPresent)
                ret.ServerCertificateValidationCallback((o, certificate, chain, errors) => true);

            return ret;
        }

        protected override void EndProcessing()
        {
            _client = null;
        }

        protected void CheckResponse(IResponse response)
        {
            WriteVerbose(response.DebugInformation);
            if (response.ServerError != null)
                throw new Exception(response.ServerError.ToString());
            CheckException(response.OriginalException);
        }

        protected Indices GetIndices(string[] index)
        {
            if (index == null || index.Length == 0)
                return Indices.All;

            return Indices.Parse(String.Join(",", index));
        }

        private void CheckException(Exception exception)
        {
            if (exception == null)
                return;

            var first = exception;

            while (exception != null)
            {
                first = exception;
                WriteVerbose($"Error: {exception.Message}\n");
                exception = exception.InnerException;
            }

            throw first;
        }

        private static Uri GetNodeUri(string node)
        {
            return new Uri(GetNodeAddress(node));
        }

        private static string GetNodeAddress(string node)
        {
            if (node.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase))
                return node;

            if (node.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase))
                return node;

            return "http://" + node;
        }
    }
}
