using System;
using Nest;

namespace Elasticsearch.Powershell.Types
{
    public class Cluster : Health
    {
        internal IConnectionSettingsValues ConnectionSettings { get; set; }
    }
}
