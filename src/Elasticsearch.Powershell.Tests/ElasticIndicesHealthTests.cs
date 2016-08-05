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
            var enumerator = cmdlet.Invoke().GetEnumerator();

            Assert.True(enumerator.MoveNext());
        }

        [Fact]
        public void ClusterHealtIndex()
        {
            string index = "test" + Guid.NewGuid();

            var create = new IndexCmdLets.ElasticNewIndex();
            create.Index = index;
            var createEnumerator = create.Invoke().GetEnumerator();
            Assert.True(createEnumerator.MoveNext());

            var health = new ElasticClusterHealth();
            health.Index = new[] { index };
            var healthEnumerator = health.Invoke().GetEnumerator();
            Assert.True(healthEnumerator.MoveNext());

            var delete = new IndexCmdLets.ElasticRemoveIndex();
            delete.Index = new[] { index };
            var deleteEnumerator = delete.Invoke().GetEnumerator();
            Assert.False(deleteEnumerator.MoveNext());
        }
    }
}
