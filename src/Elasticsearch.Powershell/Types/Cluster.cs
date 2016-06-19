using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Elasticsearch.Powershell.Types
{
    public class Cluster : Health
    {
        internal IConnectionSettingsValues ConnectionSettings { get; set; }
    }
}
