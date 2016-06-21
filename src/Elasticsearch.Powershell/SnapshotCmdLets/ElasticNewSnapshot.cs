using System;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell.SnapshotCmdLets
{
    [Cmdlet(VerbsCommon.New, "ElasticSnapshot")]
    public class ElasticNewSnapshot : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = true, HelpMessage = "The repository name")]
        public string Repository { get; set; }

        [Parameter(Position = 1, Mandatory = true, HelpMessage = "The name of the snapshot to create")]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            var response = this.Client.Snapshot(this.Repository, this.Name);
            CheckResponse(response);

            var response1 = this.Client.GetSnapshot(this.Repository, new[] { this.Name }.ToNames());
            CheckResponse(response1);

            foreach(var snapshot in response1.Snapshots)
                WriteObject(snapshot);
        }
    }
}
