using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Elasticsearch.Powershell.Tests
{
    public class IndicesHealthTests : ElasticTest
    {
        public IndicesHealthTests(ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        public void ClusterHealth()
        {
            var cmdlet = this.CreateCmdLet<ElasticClusterHealth>();
            var enumerator = cmdlet.Invoke().GetEnumerator();

            Assert.True(enumerator.MoveNext());
        }

        [Fact]
        public void NodeInfo()
        {
            var cmdlet = this.CreateCmdLet<ElasticNodeInfo>();
            var enumerator = cmdlet.Invoke().GetEnumerator();

            Assert.True(enumerator.MoveNext());
        }

        [Fact]
        public void NewGetRemoveIndex()
        {
            string index = "test" + Guid.NewGuid();

            //create index
            var create = this.CreateCmdLet<IndexCmdLets.ElasticNewIndex>();
            create.Index = index;
            var createEnumerator = create.Invoke().GetEnumerator();
            Assert.True(createEnumerator.MoveNext());

            //check index health
            var get = this.CreateCmdLet<IndexCmdLets.ElasticGetIndex>();
#if ESV1
            Assert.Equal(1, get.Invoke().Cast<Types.Index>().Count(i => i.Name.Equals(index)));
#else
            get.Index = new[] { index };
            var getEnumerator = get.Invoke().GetEnumerator();
            Assert.True(getEnumerator.MoveNext());
            Assert.False(getEnumerator.MoveNext());
#endif

            //delete index
            var delete = this.CreateCmdLet<IndexCmdLets.ElasticRemoveIndex>();
            delete.Index = new[] { index };
            var deleteEnumerator = delete.Invoke().GetEnumerator();
            Assert.False(deleteEnumerator.MoveNext());
        }
    }
}
