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
            var create = this.Client.CreateIndex(this.Index);
            this.CheckResponse(create);

#if ESV1
            var cat = this.Client.CatIndices(new CatIndicesRequest());
            this.CheckResponse(cat);
            WriteObject(new Types.Index(cat.Records.FirstOrDefault(c => c.Index.Equals(this.Index))));
#else
            var cat = this.Client.CatIndices(new CatIndicesRequest(this.Index));
            this.CheckResponse(cat);
            WriteObject(new Types.Index(cat.Records.First()));
#endif
        }
    }
}
