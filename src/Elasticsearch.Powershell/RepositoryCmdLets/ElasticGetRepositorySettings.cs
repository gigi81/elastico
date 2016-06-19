using System;
using System.Collections.Generic;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell.RepositoryCmdLets
{
    [Cmdlet(VerbsCommon.Get, "ElasticRepositorySettings")]
    public class ElasticGetRepositorySettings : ElasticGetRepository
    {
        protected override void ProcessRecord()
        {
            var response = this.Client.GetRepository(this.GetRequest);
            CheckResponse(response);

            foreach (var repo in response.Repositories.Keys)
                WriteObject(response.GetSettings(repo));
        }
    }
}
