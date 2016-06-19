using System;
using System.Management.Automation;

namespace Elasticsearch.Powershell.HealtCmdLet
{
    [Cmdlet(VerbsCommon.Get, "ElasticClusterHealth")]
    public class ElasticClusterHealth : ElasticCmdlet
    {
        protected override void ProcessRecord()
        {
            var health = this.Client.ClusterHealth();
            this.CheckResponse(health);

            WriteObject(new PSObject(new
            {
                health.ActivePrimaryShards,
                health.ActiveShards,
                health.ClusterName,
                health.InitializingShards,
                health.NumberOfDataNodes,
                health.NumberOfNodes,
                health.NumberOfPendingTasks,
                health.RelocatingShards,
                health.UnassignedShards,
                health.Status
            }));
        }
    }
}
