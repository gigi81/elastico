using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell
{
    [Cmdlet(VerbsCommon.Get, "ElasticClusterHealt")]
    public class ElasticClusterHealt : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = false, HelpMessage = "One or more index name(s). You can use the wildcard '*' in the name.")]
        public string[] Index { get; set; }

        private Indices GetIndices()
        {
            if (this.Index == null || this.Index.Length == 0)
                return Indices.All;

            return Indices.Parse(String.Join(",", this.Index));
        }

        protected override void ProcessRecord()
        {
            var health = this.Client.ClusterHealth(h => h.Index(this.GetIndices()));
            this.CheckResponse(health);

            WriteObject(new Types.Cluster
            {
                ConnectionSettings = this.Client.ConnectionSettings,
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
