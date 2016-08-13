using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
#if ESV1
            var request = new UpdateSettingsRequest()
            {
                Index = this.Index
            };

            var response = this.Client.UpdateSettings(request.SetJsonProperties(this.Settings.ToDictionary()));
#else
            var request = new UpdateIndexSettingsRequest(this.Index)
            {
                IndexSettings = new DynamicIndexSettings(this.Settings.ToDictionary())
            };

            var response = this.Client.UpdateIndexSettings(request);
#endif
            this.CheckResponse(response);
        }
    }
}
