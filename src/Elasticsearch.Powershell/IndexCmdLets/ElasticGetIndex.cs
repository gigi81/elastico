using System;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell.IndexCmdLets
{
    [Cmdlet(VerbsCommon.Get, "ElasticIndex")]
    public class ElasticGetIndex : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = false, HelpMessage = "One or more index name(s). You can use the wildcard '*' in the name.")]
        public string[] Index { get; set; }

        private Indices GetIndices()
        {
            if (this.Index == null || this.Index.Length == 0)
                return Indices.All;

            return Indices.Parse(String.Join(",", this.Index));
        }

        protected override void ProcessRecord()
        {
            var indices = this.GetIndices();
            var cat = this.Client.CatIndices(new CatIndicesRequest(indices));
            this.CheckResponse(cat);

            foreach (var index in cat.Records)
                WriteObject(new Types.Index(index));
        }
    }
}
