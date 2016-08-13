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
            var response = this.Client.GetIndexSettings(r => r.Index(this.Index));
            this.CheckResponse(response);

#if ESV1
            WriteObject(new Hashtable((IDictionary) response.IndexSettings.Settings));
#else
            foreach(var state in response.Indices.Values)
                WriteObject(new Hashtable((IDictionary) state.Settings));
#endif
        }
    }
}
