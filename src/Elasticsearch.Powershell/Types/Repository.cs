using System;

namespace Elasticsearch.Powershell.Types
{
    public class Repository
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public object Settings { get; set; }
    }
}
