using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
using Xunit;

namespace Elasticsearch.Powershell.Tests
{
    public class ElasticIndicesHealthTests
    {
        [Fact]
        public void Test()
        {
            var cmdlet = new HealtCmdLet.ElasticClusterHealth();
            var enumerator = cmdlet.Invoke().GetEnumerator();

            Assert.True(enumerator.MoveNext());
        }

        [Fact]
        public void Test2()
        {
            var cmdlet = new HealtCmdLet.ElasticIndicesHealth();
            var enumerator = cmdlet.Invoke().GetEnumerator();

            Assert.True(enumerator.MoveNext());
        }
    }
}
