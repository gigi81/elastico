using System;
using System.Linq;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell.IndexCmdLets
{
    /// <summary>
    /// <para type="synopsis">Removes one or more indices from the cluster</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "ElasticIndex", ConfirmImpact = ConfirmImpact.Medium)]
    public class ElasticRemoveIndex : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = false, HelpMessage = "One or more index name. You can use the wildcard '*' in the name.")]
        public string[] Index { get; set; }

        [Parameter(ValueFromPipeline = true)]
        public Types.Index[] InputObject { get; set; }

        private Indices GetIndices()
        {
            if (this.InputObject != null)
                return Indices.Parse(String.Join(",", this.InputObject.Select(i => i.Name)));

            if (this.Index == null || this.Index.Length == 0)
                return null;

            return Indices.Parse(String.Join(",", this.Index));
        }

        protected override void ProcessRecord()
        {
            var indices = this.GetIndices();
            if (indices == null)
                return;

            var delete = this.Client.DeleteIndex(indices);
            this.CheckResponse(delete);
        }
    }
}
