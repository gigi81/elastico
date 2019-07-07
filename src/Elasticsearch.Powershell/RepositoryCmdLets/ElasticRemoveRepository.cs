using System;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell.RepositoryCmdLets
{
    /// <summary>
    /// <para type="synopsis">Removes one or more repository from an elasticsearch cluster</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, Consts.Prefix + "Repository", ConfirmImpact = ConfirmImpact.Medium)]
    public class ElasticRemoveRepository : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = true, HelpMessage = "The name(s) of the repository to delete")]
        public string[] Repository { get; set; }

        protected override void ProcessRecord()
        {
            if (this.Repository == null || this.Repository.Length == 0)
                return;

#if ESV2 || ESV5 || ESV6
            var response = this.Client.DeleteRepository(Names.Parse(String.Join(",", this.Repository)));
#else
            var response = this.Client.Snapshot.DeleteRepository(Names.Parse(String.Join(",", this.Repository)));
#endif
            CheckResponse(response);
        }
    }
}
