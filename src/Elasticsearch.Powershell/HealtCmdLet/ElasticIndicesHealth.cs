using System;
using System.Management.Automation;

namespace Elasticsearch.Powershell.HealtCmdLet
{
    [Cmdlet(VerbsCommon.Get, "ElasticIndicesHealth")]
    public class ElasticIndicesHealth : ElasticCmdlet
    {
        protected override void ProcessRecord()
        {
            var healt = this.Client.ClusterHealth(h => h.AllIndices());
            this.CheckResponse(healt);

            if (healt.Indices?.Values == null)
                return;

            foreach(var index in healt.Indices.Values)
            {
                WriteObject(new PSObject(new
                {
                    index.ActivePrimaryShards,
                    index.ActiveShards,
                    index.InitializingShards,
                    index.NumberOfReplicas,
                    index.NumberOfShards,
                    index.RelocatingShards,
                    index.Status,
                    index.UnassignedShards
                }));
            }
        }
    }
}
