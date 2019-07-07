using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Nest;

namespace Elasticsearch.Powershell.IndexCmdLets
{
    /// <summary>
    /// <para type="synopsis">Set the specified index settings</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, Consts.Prefix + "IndexSettings")]
    public class ElasticSetIndexSettings : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = true, HelpMessage = "Index name")]
        public string Index { get; set; }

        [Parameter(Position = 2, Mandatory = true, HelpMessage = "Settings to update")]
        public Hashtable Settings { get; set; }

        protected override void ProcessRecord()
        {
            var request = new UpdateIndexSettingsRequest(this.Index)
            {
                IndexSettings = new DynamicIndexSettings(this.Settings.ToDictionary())
            };

#if ESV2 || ESV5 || ESV6
            var response = this.Client.UpdateIndexSettings(request);
#else
            var response = this.Client.Indices.UpdateSettings(request);
#endif
            this.CheckResponse(response);
        }
    }
}
