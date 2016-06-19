using System;
using Nest;

namespace Elasticsearch.Powershell.Types
{
    public class Node
    {
        public Node()
        {
        }

        internal Node(NodeInfo info)
        {
            this.Name = info.Name;
            this.Address = new Uri("http://" + info.HttpAddress);
            this.Version = info.Version;
        }

        public string Name { get; set; }

        public Uri Address { get; set; }

        public string Version { get; set; }

        public override string ToString()
        {
            return this.Address.ToString();
        }
    }
}
