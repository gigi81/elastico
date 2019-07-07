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
        protected override void ProcessRecord()
        {
#if ESV2 || ESV5 || ESV6
            var response = this.Client.GetRepository(this.GetRequest);
#else
            var response = this.Client.Snapshot.GetRepository(this.GetRequest);
#endif
            CheckResponse(response);

            foreach (var repo in response.Repositories.Keys)
                WriteObject(response.GetSettings(repo));
        }
    }
}
