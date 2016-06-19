using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Elasticsearch.Powershell
{
    [Cmdlet(VerbsCommon.New, "ElasticSnapshot")]
    public class ElasticSnapshot : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = false, HelpMessage = "")]
        public string Name { get; set; }

        [Parameter(Position = 2, Mandatory = false, HelpMessage = "")]
        public string Repository { get; set; }

        protected override void ProcessRecord()
        {
            var snap = this.Client.Snapshot(new Nest.Name(this.Repository), new Nest.Name(this.Name));
            CheckResponse(snap);

            WriteObject(snap.Snapshot);
        }
    }
}
