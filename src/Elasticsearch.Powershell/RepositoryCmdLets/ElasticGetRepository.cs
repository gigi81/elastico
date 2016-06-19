using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Elasticsearch.Powershell.RepositoryCmdLets
{
    public class ElasticGetRepository : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = false, HelpMessage = "One or more index name(s). You can use the wildcard '*' in the name.")]
        public string[] Repository { get; set; }

        private IGetRepositoryRequest GetRequest(GetRepositoryDescriptor descriptor)
        {
            if (this.Repository == null || this.Repository.Length == 0)
                return new GetRepositoryRequest();

            return new GetRepositoryRequest(Names.Parse(String.Join(",", this.Repository)));
        }

        protected override void ProcessRecord()
        {
            var response = this.Client.GetRepository(this.GetRequest);
            CheckResponse(response);

            foreach(var repo in response.Repositories)
            {
                WriteObject(repo.Value);
            }
        }
    }
}
