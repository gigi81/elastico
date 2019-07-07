using System;
using System.Management.Automation;

namespace Elasticsearch.Powershell
{
    /// <summary>
    /// <para type="synopsis">Get the cluster's nodes information</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, Consts.Prefix + "NodeInfo")]
    public class ElasticNodeInfo : ElasticCmdlet
    {
        [Parameter(ValueFromPipeline = true, HelpMessage = "The target cluster. Use this parameter instead of the Node parameter.")]
        public Types.Cluster InputObject { get; set; }

        protected override void ProcessRecord()
        {
            if (this.InputObject != null)
                this.ConnectionSettings = this.InputObject.ConnectionSettings;

#if ESV2 || ESV5 || ESV6
            var nodes = this.Client.NodesInfo();
#else
            var nodes = this.Client.Nodes.Info();
#endif
            this.CheckResponse(nodes);

            foreach (var node in nodes.Nodes.Values)
                WriteObject(node);
        }
    }
}
