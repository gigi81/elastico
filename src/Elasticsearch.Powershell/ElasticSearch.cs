using System;
using System.Dynamic;
using System.Linq;
using System.Management.Automation;
using Nest;

namespace Elasticsearch.Powershell
{
    /// <summary>
    /// <para type="synopsis">Search records in an elasticsearch cluster</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Search, Consts.Prefix)]
    public class ElasticSearch : ElasticCmdlet
    {
        public ElasticSearch()
        {
            this.Size = 100;
        }

        [Parameter(Position = 1, Mandatory = false, HelpMessage = "The elasticsearch query")]
        public string Query { get; set; }

        [Parameter(Position = 2, Mandatory = false, HelpMessage = "One or more index name(s). You can use the wildcard '*' in the name.")]
        public string[] Index { get; set; }

        [Parameter(Position = 3, Mandatory = false, HelpMessage = "The fields to return. If not specified, all fields will be returned")]
        public string[] Fields { get; set; }

        [Parameter(Position = 4, Mandatory = false, HelpMessage = "Number of records to return (default 100)")]
        public int Size { get; set; }

        private static string[] GetFields(string[] fields)
        {
            if (fields == null || fields.Length == 0)
                return null;

            if (fields.Length == 1)
                fields = fields[0].Split(',');

            fields = fields.Select(f => f.Trim())
                           .Where(f => !String.IsNullOrWhiteSpace(f))
                           .ToArray();

            if (fields.Length == 0)
                return null;

            return fields;
        }

        protected override void ProcessRecord()
        {
#if ESV1
            var search = new SearchDescriptor<ExpandoObject>()
                                 .AllTypes()
                                 .Indices(GetIndices(this.Index))
                                 .Size(this.Size);
#else
            var search = new SearchDescriptor<ExpandoObject>()
                                 .AllTypes()
                                 .Index(GetIndices(this.Index))
                                 .Size(this.Size);
#endif
            if (!String.IsNullOrWhiteSpace(this.Query))
                search = search.Query(q => q.QueryString(s => s.Query(this.Query)));

            var include = GetFields(this.Fields);
            if (include != null)
            {
#if ESV1
                search = search.Source(s => s.Include(include));
#elif ESV2
                search = search.Source(s => s.Include(i => i.Fields(include)));
#else
                search = search.Source(s => s.Includes(i => i.Fields(include)));
#endif
            }

            var response = this.Client.Search<ExpandoObject>(search);
            this.CheckResponse(response);

            foreach (var document in response.Documents)
                WriteObject(document.ToPSObject());
        }
    }
}
