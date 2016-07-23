using System;
using System.Linq;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell.IndexCmdLet
{
    /// <summary>
    /// <para type="synopsis">Creates a new index in the cluster</para>
    /// </summary>
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
