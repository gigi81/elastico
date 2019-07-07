﻿using System;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell.SnapshotCmdLets
{
    /// <summary>
    /// <para type="synopsis">Delete snapshots from the specified repository</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, Consts.Prefix + "Snapshot")]
    public class ElasticRemoveSnapshot : ElasticCmdlet
    {
        [Parameter(Position = 1, Mandatory = true, HelpMessage = "The repository name")]
        public string Repository { get; set; }

        [Parameter(Position = 2, Mandatory = true, HelpMessage = "One or more snapshot name(s) to delete")]
        public string[] Name { get; set; }

        protected override void ProcessRecord()
        {
            foreach(var name in this.Name)
            {
#if ESV2 || ESV5 || ESV6
                var response = this.Client.DeleteSnapshot(this.Repository, name);
#else
                var response = this.Client.Snapshot.Delete(this.Repository, name);
#endif
                CheckResponse(response);
            }
        }
    }
}
