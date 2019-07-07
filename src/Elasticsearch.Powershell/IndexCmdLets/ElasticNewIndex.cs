using System;
using System.Linq;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell.IndexCmdLets
{
    /// <summary>
    /// <para type="synopsis">Creates a new index in the cluster</para>
    /// </summary>
    [Cmdlet(VerbsCommon.New, Consts.Prefix + "Index")]
    public class ElasticNewIndex : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = true, HelpMessage = "The name of the new index to create")]
        public string Index { get; set; }

        protected override void ProcessRecord()
        {
#if ESV2 || ESV5 || ESV6
            var create = this.Client.CreateIndex(this.Index);
            this.CheckResponse(create);
            var cat = this.Client.CatIndices(new CatIndicesRequest(this.Index));
#else
            var create = this.Client.Indices.Create(this.Index);
            this.CheckResponse(create);
            var cat = this.Client.Cat.Indices(new CatIndicesRequest(this.Index));
#endif

            this.CheckResponse(cat);
            WriteObject(new Types.Index(cat.Records.First()));
        }
    }
}
