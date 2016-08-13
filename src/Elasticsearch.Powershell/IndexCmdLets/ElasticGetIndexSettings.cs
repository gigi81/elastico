using System;
using System.Collections;
using System.Management.Automation;

namespace Elasticsearch.Powershell.IndexCmdLets
{
    /// <summary>
    /// <para type="synopsis">Get index settings. V1 cmdlet is single index, while V2+ supports multiple indexes per reuqest</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, Consts.Prefix + "IndexSettings")]
    public class ElasticGetIndexSettings : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = true, HelpMessage = "Index name")]
        public string Index { get; set; }

        protected override void ProcessRecord()
        {
            var cat = this.Client.GetIndexSettings(r => r.Index(this.Index));
            this.CheckResponse(cat);

#if ESV1
            WriteObject(new Hashtable((IDictionary) cat.IndexSettings.Settings));
#else
            foreach(var state in cat.Indices.Values)
                WriteObject(new Hashtable((IDictionary) state.Settings));
#endif
        }
    }
}
