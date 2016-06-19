using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Powershell.Types
{
    public class Health
    {
        public int ActivePrimaryShards { get; set; }
        public int ActiveShards { get; set; }
        public string Name { get; set; }
        public int InitializingShards { get; set; }
        public int NumberOfDataNodes { get; set; }
        public int NumberOfNodes { get; set; }
        public int NumberOfPendingTasks { get; set; }
        public int RelocatingShards { get; set; }
        public int UnassignedShards { get; set; }
        public string Status { get; set; }
    }
}
