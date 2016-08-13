using System;
using System.Linq;
using System.Management.Automation;
#if ESV1
using Elasticsearch.Net.ConnectionPool;
#else
using Elasticsearch.Net;
#endif
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

        protected IElasticClient Client
        {
            get
            {
                if (_client != null)
                    return _client;

                return _client = new ElasticClient(this.ConnectionSettings);
            }
        }

        protected IConnectionSettingsValues ConnectionSettings
        {
            get
            {
                if (_connectionSettings != null)
                    return _connectionSettings;

                var pool = new StaticConnectionPool(this.Node.Select(GetNodeUri));
                return _connectionSettings = new ConnectionSettings(pool);
            }
            set
            {
                _connectionSettings = value;
            }
        }

        protected override void EndProcessing()
        {
            _client = null;
        }

#if ESV1
        protected void CheckResponse(IResponse response)
        {
            WriteVerbose(response.RequestInformation?.Metrics?.ToString());
            CheckException(response.RequestInformation?.OriginalException);
        }
#else
        protected void CheckResponse(IResponse response)
        {
            WriteVerbose(response.DebugInformation);
            if (response.ServerError != null)
                throw new Exception(response.ServerError.ToString());
            CheckException(response.OriginalException);
        }
#endif

#if ESV1
        protected static string[] GetIndices(string[] index)
        {
            if (index == null || index.Length == 0)
                return new[] { "*" };

            return index;
        }
#else
        protected Indices GetIndices(string[] index)
        {
            if (index == null || index.Length == 0)
                return Indices.All;

            return Indices.Parse(String.Join(",", index));
        }
#endif

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
