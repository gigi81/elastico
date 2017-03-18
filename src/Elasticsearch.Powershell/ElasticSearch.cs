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
        private const string ScrollTimeout = "10s"; //10 seconds

        public ElasticSearch()
        {
            this.Size = 100;
            this.From = 0;
        }

        [Parameter(Position = 1, Mandatory = false, ParameterSetName = "Search", HelpMessage = "The elasticsearch query")]
        public string Query { get; set; }

        [Parameter(Position = 2, Mandatory = false, ParameterSetName = "Search", HelpMessage = "One or more index name(s). You can use the wildcard '*' in the name.")]
        public string[] Index { get; set; }

        [Parameter(Position = 3, Mandatory = false, ParameterSetName = "Search", HelpMessage = "The fields to return. If not specified, all fields will be returned")]
        public string[] Fields { get; set; }

        [Parameter(Position = 4, Mandatory = false, ParameterSetName = "Search", HelpMessage = "Number of records to return (default 100)")]
        public int Size { get; set; }

        [Parameter(Position = 5, Mandatory = false, ParameterSetName = "Search", HelpMessage = "The starting from index of the hits to return (default 0)")]
        public int From { get; set; }

        [Parameter(Position = 6, Mandatory = false, ParameterSetName = "Search", HelpMessage = "Scroll Switch (enables the scroll API to retrieve a large number of records)")]
        public SwitchParameter Scroll;

        [Parameter(Position = 7, Mandatory = false, ParameterSetName = "Scroll", HelpMessage = "Scroll Id")]
        public string ScrollId;

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
            if (this.ParameterSetName.Equals("Scroll"))
                ScrollInternal();
            else
                SearchInternal();
        }

        private void SearchInternal()
        {
#if ESV1
            var search = new SearchDescriptor<ExpandoObject>()
                                 .AllTypes()
                                 .Indices(GetIndices(this.Index))
                                 .From(this.From)
                                 .Size(this.Size);
#else
            var search = new SearchDescriptor<ExpandoObject>()
                                 .AllTypes()
                                 .Index(GetIndices(this.Index))
                                 .From(this.From)
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

            if (this.Scroll.IsPresent)
            {
#if ESV1 || ESV2
                search = search.SearchType(Net.SearchType.Scan)
                               .Scroll(ScrollTimeout);
#else
                search = search.Scroll(ScrollTimeout);
#endif
            }

            var response = this.Client.Search<ExpandoObject>(search);
            this.CheckResponse(response);

            if (this.Scroll.IsPresent)
            {
                WriteObject(response.ScrollId);
            }
            else
            {
                foreach (var document in response.Documents)
                    WriteObject(document.ToPSObject());
            }
        }

        private void ScrollInternal()
        {
            var response = this.Client.Scroll<ExpandoObject>(ScrollTimeout, this.ScrollId);
            this.CheckResponse(response);

            foreach (var document in response.Documents)
                WriteObject(document.ToPSObject());
        }
    }
}
