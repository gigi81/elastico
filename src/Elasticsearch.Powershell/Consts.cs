using System;
using System.Collections.Generic;
using System.Text;

namespace Elasticsearch.Powershell
{
    internal static class Consts
    {
#if ESV1
        public const string Prefix = "ElasticV1";
#endif

#if ESV2
        public const string Prefix = "ElasticV2";
#endif
    }
}
