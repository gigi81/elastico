using System;
using System.Threading;
using Nest;
using Xunit;
using Xunit.Abstractions;

namespace Elasticsearch.Powershell.Tests
{
    public class ElasticSearchTests : IDisposable
    {
        ElasticClient _client;
        string index = "searchtests" + Guid.NewGuid();
        ITestOutputHelper _output;

        public ElasticSearchTests(ITestOutputHelper output)
        {
            _output = output;

            var settings = new ConnectionSettings(new Uri("http://localhost:9200"));
            settings.DefaultIndex(index);

            _client = new ElasticClient(settings);

            var person = new Domain.Person
            {
                Id = 1,
                Firstname = "Pinco",
                Lastname = "Pallino"
            };

            var insertResponse = _client.Index(person);
            if (!insertResponse.IsValid)
                throw insertResponse.OriginalException;

            Thread.Sleep(1000);
        }

        public void Dispose()
        {
            _client.DeleteIndex(index);
        }

        [Fact]
        public void Search()
        {
            var cmdlet = new ElasticSearch();
            cmdlet.Index = new[] { index };
            var enumerator = cmdlet.Invoke().GetEnumerator();
            var found = 0;

            while(enumerator.MoveNext())
            {
                _output.WriteLine(enumerator.Current.ToString());
                found++;
            }

            _output.WriteLine($"Found {found} records");
            Assert.True(found > 0);
        }
    }
}
