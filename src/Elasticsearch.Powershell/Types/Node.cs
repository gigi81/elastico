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
#if ESV1
            this.Address = new Uri("http://" + info.HttpAddress);
#else
            this.Address = new Uri("http://" + info.Http.PublishAddress);
#endif
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
