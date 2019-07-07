using System;
using System.Collections;
using System.Management.Automation;

namespace Elasticsearch.Powershell.IndexCmdLets
{
    /// <summary>
    /// <para type="synopsis">Get the specified index settings</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, Consts.Prefix + "IndexSettings")]
    public class ElasticGetIndexSettings : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = true, HelpMessage = "Index name")]
        public string Index { get; set; }

        protected override void ProcessRecord()
        {
#if ESV2 || ESV5 || ESV6
            var response = this.Client.GetIndexSettings(r => r.Index(this.Index));
#else
            var response = this.Client.Indices.GetSettings(this.Index);
#endif
            this.CheckResponse(response);

            foreach (var state in response.Indices.Values)
                WriteObject(state.Settings?.ReflectToPSObject());
        }
    }
}
