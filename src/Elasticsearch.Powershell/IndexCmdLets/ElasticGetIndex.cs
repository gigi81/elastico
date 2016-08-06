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
#if !ESV1 //V1 does not support cat filtering on the request
        [Parameter(Position = 1, Mandatory = false, HelpMessage = "One or more index name(s). You can use the wildcard '*' in the name.")]
        public string[] Index { get; set; }
#endif

        protected override void ProcessRecord()
        {
#if ESV1
            var request = new CatIndicesRequest();
#else
            var request = new CatIndicesRequest(GetIndices(this.Index));
#endif
            var cat = this.Client.CatIndices(request);
            this.CheckResponse(cat);

            foreach (var index in cat.Records)
                WriteObject(new Types.Index(index));
        }
    }
}
