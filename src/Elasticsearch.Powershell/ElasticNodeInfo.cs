using System;
using System.Management.Automation;

namespace Elasticsearch.Powershell
{
    [Cmdlet(VerbsCommon.Get, "ElasticNodeInfo")]
    public class ElasticNodeInfo : ElasticCmdlet
    {
        [Parameter(ValueFromPipeline = true)]
        public Types.Cluster InputObject { get; set; }

        protected override void ProcessRecord()
        {
            if (this.InputObject != null)
                this.ConnectionSettings = this.InputObject.ConnectionSettings;

            var nodes = this.Client.NodesInfo();
            this.CheckResponse(nodes);

            foreach (var node in nodes.Nodes.Values)
                WriteObject(node);
        }
    }
}
