using System;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell.SnapshotCmdLets
{
    /// <summary>
    /// <para type="synopsis">Get a list of the spanpshots in the specified repository</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, Consts.Prefix + "Snapshot")]
    public class ElasticGetSnapshot : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = true, HelpMessage = "The repository name")]
        public string Repository { get; set; }

        [Parameter(Position = 2, Mandatory = false, HelpMessage = "One or more snapshot name(s)")]
        public string[] Name { get; set; }

        protected override void ProcessRecord()
        {
            var response = this.Client.GetSnapshot(this.Repository, this.Name.ToNames(defaultName: "*"));
            CheckResponse(response);

            foreach (var snapshot in response.Snapshots)
                WriteObject(snapshot);
        }
    }
}
