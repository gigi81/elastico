using System;
using Xunit;
using Xunit.Abstractions;

namespace Elasticsearch.Powershell.Tests
{
    public class ElasticIndicesHealthTests : ElasticTest
    {
        public ElasticIndicesHealthTests(ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        public void ClusterHealth()
        {
            var cmdlet = new ElasticClusterHealth();
            cmdlet.Node = this.Node;
            var enumerator = cmdlet.Invoke().GetEnumerator();

            Assert.True(enumerator.MoveNext());
        }

        [Fact]
        public void ClusterHealtIndex()
        {
            string index = "test" + Guid.NewGuid();

            var create = new IndexCmdLets.ElasticNewIndex();
            create.Node = this.Node;
            create.Index = index;
            var createEnumerator = create.Invoke().GetEnumerator();
            Assert.True(createEnumerator.MoveNext());

            var health = new ElasticClusterHealth();
            health.Node = this.Node;
            health.Index = new[] { index };
            var healthEnumerator = health.Invoke().GetEnumerator();
            Assert.True(healthEnumerator.MoveNext());

            var delete = new IndexCmdLets.ElasticRemoveIndex();
            delete.Node = this.Node;
            delete.Index = new[] { index };
            var deleteEnumerator = delete.Invoke().GetEnumerator();
            Assert.False(deleteEnumerator.MoveNext());
        }
    }
}
