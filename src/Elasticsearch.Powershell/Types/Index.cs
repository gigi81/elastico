using System;
using Nest;

namespace Elasticsearch.Powershell.Types
{
    public class Index
    {
        public Index()
        {
        }

        internal Index(CatIndicesRecord index)
        {
            this.DocsCount = long.Parse(index.DocsCount);
            this.DocsDeleted = long.Parse(index.DocsDeleted);
            this.Health = index.Health;
            this.Name = index.Index;
            this.Primary = index.Primary;
            this.PrimaryStoreSize = index.PrimaryStoreSize;
            this.Replica = index.Replica;
            this.Status = index.Status;
            this.StoreSize = index.StoreSize;
            this.TotalMemory = index.TotalMemory;
        }

        public long DocsCount { get; set; }

        public long DocsDeleted { get; set; }

        public string Health { get; set; }

        public string Name { get; set; }

        public string Primary { get; set; }

        public string PrimaryStoreSize { get; set; }

        public string Replica { get; set; }

        public string Status { get; set; }

        public string StoreSize { get; set; }

        public string TotalMemory { get; set; }
    }
}
