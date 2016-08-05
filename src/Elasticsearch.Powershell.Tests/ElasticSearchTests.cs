using System;
using System.Threading;
using Nest;
using Xunit;
using Xunit.Abstractions;

namespace Elasticsearch.Powershell.Tests
{
    public class ElasticSearchTests : ElasticTest, IDisposable
    {
        ElasticClient _client;
        string _index = "searchtests" + Guid.NewGuid();

        public ElasticSearchTests(ITestOutputHelper output)
            : base(output)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"));
#if ESV1
            settings.SetDefaultIndex(_index);
#else
            settings.DefaultIndex(_index);
#endif

            _client = new ElasticClient(settings);

            var person = new Domain.Person
            {
                Id = 1,
                Firstname = "Pinco",
                Lastname = "Pallino"
            };

            var insertResponse = _client.Index(person);
            CheckResponse(insertResponse);

            Thread.Sleep(1000);
        }

        public void Dispose()
        {
            _client.DeleteIndex(_index);
        }

        [Fact]
        public void Search()
        {
            var cmdlet = new ElasticSearch();
            cmdlet.Index = new[] { _index };
            var enumerator = cmdlet.Invoke().GetEnumerator();
            var found = 0;

            while(enumerator.MoveNext())
            {
                Output.WriteLine(enumerator.Current.ToString());
                found++;
            }

            Output.WriteLine($"Found {found} records");
            Assert.True(found > 0);
        }
    }
}
