using System;
using System.Collections;
using System.Collections.Generic;
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
            this.Nodes = new[] { "http://localhost:9200" };
        }

        [Parameter(Position = 0, Mandatory = false, HelpMessage = "The cluster node(s) urls (ex. http://localhost:9200)")]
        public string[] Nodes { get; set; }

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

                var pool = new StaticConnectionPool(this.Nodes.Select(GetNodeUri));
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

        protected void CheckResponse(IResponse response)
        {
            WriteVerbose(response.DebugInformation);

            if (response.OriginalException != null)
            {
                var exception = response.OriginalException;
                var first = exception;

                while(exception != null)
                {
                    first = exception;
                    WriteVerbose("Error: " + exception.Message);
                    exception = exception.InnerException;
                }

                throw first;
            }
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
