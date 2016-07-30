using System;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell
{
    /// <summary>
    /// <para type="synopsis">Get the cluster's health status. It's possible to filter the output for specific indices.</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "ElasticClusterHealth")]
    public class ElasticClusterHealth : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = false, HelpMessage = "One or more index name(s) to filter output only for the specified index(es). You can use the wildcard '*' in the name.")]
        public string[] Index { get; set; }

        protected override void ProcessRecord()
        {
#if ESV1
            var health = this.Client.ClusterHealth(h => h.Indices(GetIndices(this.Index)));
#else
            var health = this.Client.ClusterHealth(h => h.Index(GetIndices(this.Index)));
#endif
            this.CheckResponse(health);

            WriteObject(new Types.Cluster
            {
                ConnectionSettings = this.ConnectionSettings,
                ActivePrimaryShards = health.ActivePrimaryShards,
                ActiveShards = health.ActiveShards,
                Name = health.ClusterName,
                InitializingShards = health.InitializingShards,
                NumberOfDataNodes = health.NumberOfDataNodes,
                NumberOfNodes = health.NumberOfNodes,
                NumberOfPendingTasks = health.NumberOfPendingTasks,
                RelocatingShards = health.RelocatingShards,
                Status = health.Status,
                UnassignedShards = health.UnassignedShards
            });
        }
    }
}
