using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace Elasticsearch.Powershell.Types
{
    public class SearchResponse
    {
        public SearchResponse()
        {
            this.Documents = new PSObject[0];
        }

        public string ScrollId { get; set; }

        public long Total { get; set; }

        public PSObject[] Documents { get; set; }
    }
}
