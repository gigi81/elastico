using System;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell.RepositoryCmdLets
{
    [Cmdlet(VerbsCommon.Remove, "ElasticRepository")]
    public class ElasticRemoveRepository : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = true, HelpMessage = "The name(s) of the repository to delete")]
        public string[] Repository { get; set; }

        protected override void ProcessRecord()
        {
            if (this.Repository == null || this.Repository.Length == 0)
                return;

            var response = this.Client.DeleteRepository(Names.Parse(String.Join(",", this.Repository)));
            CheckResponse(response);
        }
    }
}
