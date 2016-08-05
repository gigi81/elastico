using System;
using System.Collections.Generic;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell.RepositoryCmdLets
{
    /// <summary>
    /// <para type="synopsis">Get the repository's settings</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, Consts.Prefix + "RepositorySettings")]
    public class ElasticGetRepositorySettings : ElasticGetRepository
    {
#if !ESV1
        protected override void ProcessRecord()
        {
            var response = this.Client.GetRepository(this.GetRequest);
            CheckResponse(response);

            foreach (var repo in response.Repositories.Keys)
                WriteObject(response.GetSettings(repo));
        }
#else
        protected override void ProcessRecord()
        {
            foreach(var request in this.GetRequest())
            {
                var response = this.Client.GetRepository(request);
                CheckResponse(response);

                foreach (var repo in response.Repositories.Values)
                    WriteObject(repo.Settings);
            }
        }
#endif
    }
}
