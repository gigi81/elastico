using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Elasticsearch.Powershell.Tests
{
    public class ElasticSearchTests
    {
        [Fact]
        public void Test()
        {
            var cmdlet = new ElastichSearch();
            cmdlet.Fields = new[] { "EventID" };
            var enumerator = cmdlet.Invoke().GetEnumerator();

            Assert.True(enumerator.MoveNext());
        }
    }
}
