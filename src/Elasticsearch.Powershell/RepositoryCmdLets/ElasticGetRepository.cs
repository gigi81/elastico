using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell.RepositoryCmdLets
{
    /// <summary>
    /// <para type="synopsis">Get the cluster's repositories</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "ElasticRepository")]
    public class ElasticGetRepository : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = false, HelpMessage = "One or more repository name(s)")]
        public string[] Repository { get; set; }

#if ESV1
        protected IEnumerable<IGetRepositoryRequest> GetRequest()
        {
            if (this.Repository == null || this.Repository.Length == 0)
                return new[] { new GetRepositoryRequest() };

            return this.Repository.Select(r => new GetRepositoryRequest(r));
        }

        protected override void ProcessRecord()
        {
            foreach (var request in this.GetRequest())
            {
                var response = this.Client.GetRepository(request);
                CheckResponse(response);

                foreach (var repo in response.Repositories)
                    WriteObject(new Types.Repository()
                    {
                        Name = repo.Key,
                        Settings = repo.Value.Settings
                    });
            }
        }
#else
        protected IGetRepositoryRequest GetRequest(GetRepositoryDescriptor descriptor)
        {
            if (this.Repository == null || this.Repository.Length == 0)
                return new GetRepositoryRequest();

            return new GetRepositoryRequest(Names.Parse(String.Join(",", this.Repository)));
        }

        protected override void ProcessRecord()
        {
            var response = this.Client.GetRepository(this.GetRequest);
            CheckResponse(response);

            foreach (var repo in response.Repositories.Keys)
                WriteObject(response.GetRepository(repo));
        }
#endif
    }
}
