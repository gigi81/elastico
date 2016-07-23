using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell.RepositoryCmdLets
{
    /// <summary>
    /// <para type="synopsis">Creates a new filesystem repository in the elasticsearch cluster</para>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "ElasticRepositoryFileSystem")]
    public class ElasticRepositoryFileSystem : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = true, HelpMessage = "Repository name")]
        public string Name { get; set; }

        [Parameter(Position = 2, Mandatory = true, HelpMessage = "Repository location")]
        public string Location { get; set; }

        [Parameter(Position = 3, Mandatory = false, HelpMessage = "Set repository compression")]
        public bool? Compress { get; set; }

        [Parameter(Position = 4, Mandatory = false, HelpMessage = "Set repository chunk size")]
        public string ChunkSize { get; set; }

        private FileSystemRepositorySettings GetSettings()
        {
            var ret = new FileSystemRepositorySettings(this.Location)
            {
                Compress = this.Compress
            };

            if (!String.IsNullOrWhiteSpace(this.ChunkSize))
                ret.ChunkSize = this.ChunkSize;

            return ret;
        }

        protected override void ProcessRecord()
        {
            //create repository
            var request = new CreateRepositoryRequest(this.Name)
            {
                Repository = new FileSystemRepository(this.GetSettings())
            };

            var response = this.Client.CreateRepository(request);
            CheckResponse(response);

            //get repository
            var response1 = this.Client.GetRepository(new GetRepositoryRequest(this.Name));
            CheckResponse(response1);

            foreach (var repo in response1.Repositories.Keys)
                WriteObject(response1.GetRepository(repo));
        }
    }
}
