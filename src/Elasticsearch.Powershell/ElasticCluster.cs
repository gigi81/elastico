using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell
{
    [Cmdlet(VerbsCommon.Get, "ElasticCluster")]
    public class ElasticCluster : ElasticCmdlet
    {
        protected override void ProcessRecord()
        {
            var health = this.Client.ClusterHealth();
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
