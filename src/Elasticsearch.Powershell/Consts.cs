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

#if ESV5
        public const string Prefix = "ElasticV5";
#endif

#if ESV6
        public const string Prefix = "ElasticV6";
#endif

#if ESV7
        public const string Prefix = "ElasticV7";
#endif
    }
}
