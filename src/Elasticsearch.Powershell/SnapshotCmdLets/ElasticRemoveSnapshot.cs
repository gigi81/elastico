using System;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell.SnapshotCmdLets
{
    /// <summary>
    /// <para type="synopsis">Delete snapshots from the specified repository</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "ElasticSnapshot")]
    public class ElasticRemoveSnapshot : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = true, HelpMessage = "The repository name")]
        public string Repository { get; set; }

        [Parameter(Position = 2, Mandatory = false, HelpMessage = "One or more snapshot name(s) to delete")]
        public string[] Name { get; set; }

        protected override void ProcessRecord()
        {
            foreach(var name in this.Name)
            {
                var response = this.Client.DeleteSnapshot(this.Repository, name);
                CheckResponse(response);
            }
        }
    }
}
