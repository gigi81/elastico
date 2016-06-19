using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Powershell;
using Nest;
using Xunit;

namespace Elasticsearch.Powershell.Tests
{
    public class ElasticIndicesHealthTests
    {
        [Fact]
        public void Test()
        {
            var cmdlet = new ElasticClusterHealt();
            var enumerator = cmdlet.Invoke().GetEnumerator();

            Assert.True(enumerator.MoveNext());
        }

        [Fact]
        public void Test2()
        {
            var cmdlet = new ElasticClusterHealt();
            cmdlet.Index = new[] { ".kibana" };
            var enumerator = cmdlet.Invoke().GetEnumerator();

            Assert.True(enumerator.MoveNext());
        }
    }
}
