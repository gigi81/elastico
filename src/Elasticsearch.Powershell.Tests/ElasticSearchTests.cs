using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Management.Automation;
using System.Threading;
using Nest;
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
            foreach(var person in Data)
            {
                var insertResponse = this.Client.Index(person);
                CheckResponse(insertResponse);
            }

            //wait for server to index data
            Thread.Sleep(1000);
        }

        [Fact]
        public void SearchAll()
        {
            var cmdlet = this.CreateCmdLet<ElasticSearch>();
            cmdlet.Index = new[] { this.DefaultIndex };
            var enumerator = cmdlet.Invoke().GetEnumerator();
            var found = 0;

            while(enumerator.MoveNext())
            {
                Output.WriteLine(enumerator.Current.ToString());
                found++;
            }

            Output.WriteLine($"Found {found} records");
            Assert.Equal(Data.Length, found);
        }

        [Fact]
        public void SearchQuery()
        {
            var field = nameof(Domain.Person.Firstname);
            var value = Data[0].Firstname;
            var count = Data.Count(p => p.Firstname.Equals(value));

            Assert.Equal(1, count);

            var cmdlet = this.CreateCmdLet<ElasticSearch>();
            cmdlet.Index = new[] { this.DefaultIndex };
            cmdlet.Query = $"{field}:{value}";
            var enumerator = cmdlet.Invoke().GetEnumerator();
            var found = 0;

            while (enumerator.MoveNext())
            {
                Output.WriteLine(enumerator.Current.ToString());

                var record = (PSObject) enumerator.Current;
                Assert.Equal(value, record.Properties[field].Value);
                found++;
            }

            Output.WriteLine($"Found {found} records");
            Assert.Equal(count, found);
        }

        [Fact]
        public void SearchFields()
        {
            var field = nameof(Domain.Person.Firstname);

            var cmdlet = this.CreateCmdLet<ElasticSearch>();
            cmdlet.Index = new[] { this.DefaultIndex };
            cmdlet.Fields = new[] { field };
            var found = 0;

            foreach(PSObject record in cmdlet.Invoke())
            {
                Output.WriteLine(record.ToString());
                Assert.NotNull(record.Properties[field]);
                Assert.Equal(cmdlet.Fields.Length, record.Properties.Count());
                found++;
            }

            Output.WriteLine($"Found {found} records");
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
