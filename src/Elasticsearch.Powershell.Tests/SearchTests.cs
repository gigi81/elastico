using Elasticsearch.Powershell.Types;
using System;
using System.Linq;
using System.Management.Automation;
using Xunit;
using Xunit.Abstractions;

namespace Elasticsearch.Powershell.Tests
{
    public class ElasticSearchTests : ElasticTest
    {
        private static readonly Domain.Person[] Data = CreateData();

        public ElasticSearchTests(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Init()
        {
            foreach (var person in Data)
            {
#if ESV2 || ESV5
                var insertResponse = this.Client.Index(person);
#else
                var insertResponse = this.Client.IndexDocument(person);
#endif
                CheckResponse(insertResponse);
            }

            this.RefreshIndex();
        }

        private SearchResponse GetResponse(ElasticSearch cmdlet)
        {
            var enumerator = cmdlet.Invoke().GetEnumerator();
            enumerator.MoveNext();
            return (SearchResponse)enumerator.Current;
        }

        [Fact]
        public void SearchAll()
        {
            var cmdlet = this.CreateCmdLet<ElasticSearch>();
            cmdlet.Index = new[] { this.DefaultIndex };
            var response = GetResponse(cmdlet);

            Assert.Equal(Data.Length, response.Documents.Length);
            Assert.Equal(Data.Length, response.Total);
        }

        [Fact]
        public void SearchQuery()
        {
            //fields are camel case
            //https://www.elastic.co/guide/en/elasticsearch/client/net-api/2.x/field-inference.html
            var field = nameof(Domain.Person.Firstname).ToLower();

            var value = Data[0].Firstname;
            var count = Data.Count(p => p.Firstname.Equals(value));

            Assert.Equal(1, count);

            var cmdlet = this.CreateCmdLet<ElasticSearch>();
            cmdlet.Index = new[] { this.DefaultIndex };
            cmdlet.Query = $"{field}:{value}";
            var response = GetResponse(cmdlet);

            foreach (PSObject record in response.Documents)
                Assert.Equal(value, record.Properties[field].Value);

            Assert.Equal(count, response.Documents.Length);
            Assert.Equal(count, response.Total);
        }

        [Fact]
        public void SearchFields()
        {
            //fields are camel case
            //https://www.elastic.co/guide/en/elasticsearch/client/net-api/2.x/field-inference.html
            var field = nameof(Domain.Person.Firstname).ToLower();

            var cmdlet = this.CreateCmdLet<ElasticSearch>();
            cmdlet.Index = new[] { this.DefaultIndex };
            cmdlet.Fields = new[] { field };
            var response = GetResponse(cmdlet);

            foreach (PSObject record in response.Documents)
            {
                Assert.NotNull(record.Properties[field]);
                Assert.Equal(cmdlet.Fields.Length, record.Properties.Count());
            }

            Assert.Equal(Data.Length, response.Documents.Length);
            Assert.Equal(Data.Length, response.Total);
        }

        [Fact]
        public void ScrollApiTest()
        {
            var cmdlet = this.CreateCmdLet<ElasticSearch>();
            cmdlet.Index = new[] { this.DefaultIndex };
            cmdlet.Scroll = new SwitchParameter(true);
            var response = GetResponse(cmdlet);

            Assert.True(!String.IsNullOrWhiteSpace(response.ScrollId));
            Assert.Equal(Data.Length, response.Total);

            var found = response.Documents.Length;

            while(found < response.Total)
            {
                var cmdlet2 = this.CreateCmdLet<ElasticSearch>();
                cmdlet2.ScrollId = response.ScrollId;
                var response2 = GetResponse(cmdlet2);

                found += response2.Documents.Length;
            }

            _output.WriteLine($"Found {found} records");
            Assert.Equal(Data.Length, found);
        }

        private static Domain.Person[] CreateData()
        {
            return new[]
            {
                new Domain.Person
                {
                    Id = 1,
                    Firstname = "Pinco",
                    Lastname = "Pallino"
                },

                new Domain.Person
                {
                    Id = 2,
                    Firstname = "John",
                    Lastname = "Doe"
                },

                new Domain.Person
                {
                    Id = 3,
                    Firstname = "Luigi",
                    Lastname = "Grilli"
                }
            };
        }
    }
}
