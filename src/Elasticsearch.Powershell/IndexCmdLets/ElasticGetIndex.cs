using System;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell.IndexCmdLets
{
    /// <summary>
    /// <para type="synopsis">Get the cluster's indices. The output can be filtered using the Index parameter.</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, Consts.Prefix + "Index")]
    public class ElasticGetIndex : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = false, HelpMessage = "One or more index name(s). You can use the wildcard '*' in the name.")]
        public string[] Index { get; set; }

        protected override void ProcessRecord()
        {
            var request = new CatIndicesRequest(GetIndices(this.Index));
#if ESV2 || ESV5 || ESV6
            var cat = this.Client.CatIndices(request);
#else
            var cat = this.Client.Cat.Indices(request);
#endif

            this.CheckResponse(cat);

            foreach (var index in cat.Records)
                WriteObject(new Types.Index(index));
        }
    }
}
