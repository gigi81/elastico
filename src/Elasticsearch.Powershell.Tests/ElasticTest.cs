using System;
using Nest;
using Xunit.Abstractions;

namespace Elasticsearch.Powershell.Tests
{
    public class ElasticTest : IDisposable
    {
        protected readonly ITestOutputHelper Output;
        private readonly ElasticsearchInside.Elasticsearch Server;
        private ElasticClient _client;
        private string _index = "searchtests" + Guid.NewGuid();

        protected ElasticTest(ITestOutputHelper outputHelper)
        {
            this.Output = outputHelper;
            this.Server = new ElasticsearchInside.Elasticsearch(c => c.EnableLogging().LogTo(outputHelper.WriteLine));
        }

        public Uri ServerUrl
        {
            get { return this.Server.Url; }
        }

        public string[] Node
        {
            get { return new[] { this.ServerUrl.ToString() }; }
        }

        public ElasticClient Client
        {
            get
            {
                if (_client != null)
                    return _client;

                var settings = new ConnectionSettings(this.ServerUrl);
#if ESV1
                settings.SetDefaultIndex(this.DefaultIndex);
#else
                settings.DefaultIndex(this.DefaultIndex);
#endif

                return _client = new ElasticClient(settings);
            }
        }

        public string DefaultIndex {  get { return _index; } }

        public void Dispose()
        {
            try
            {
                this.DisposeInternal();
            }
            finally
            {
                this.Server.Dispose();
            }
        }

        protected TCmdLet CreateCmdLet<TCmdLet>() where TCmdLet : ElasticCmdlet, new()
        {
            var ret = new TCmdLet();
            ret.Node = this.Node;
            return ret;
        }

        protected virtual void DisposeInternal()
        {
        }

        protected void CheckResponse(IResponse response)
        {
#if ESV1
            var exception = response?.RequestInformation?.OriginalException;
#else
            var exception = response.OriginalException;
#endif
            if (!response.IsValid)
                throw exception ?? new Exception("Request failed");
        }
    }
}
