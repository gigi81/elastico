using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Elasticsearch.Powershell.IndexCmdLet
{
    [Cmdlet(VerbsCommon.New, "ElasticIndex")]
    public class ElasticNewIndex : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = true, HelpMessage = "The name of the new index to create")]
        public string Index { get; set; }

        protected override void ProcessRecord()
        {
            var index = new IndexName { Name = this.Index };

            var create = this.Client.CreateIndex(index);
            this.CheckResponse(create);

            var cat = this.Client.CatIndices(new CatIndicesRequest(index));
            this.CheckResponse(cat);

            WriteObject(new Types.Index(cat.Records.First()));
        }
    }
}
