using System;
using System.Threading.Tasks;
using Nest;
using Xunit.Abstractions;
using Xunit;

namespace Elasticsearch.Powershell.Tests
{
    public class ElasticTest : IAsyncLifetime
    {
        protected readonly ITestOutputHelper _output;

        private readonly ElasticsearchInside.Elasticsearch _server;
        private ElasticClient _client;
        private string _index = "searchtests" + Guid.NewGuid();

        protected ElasticTest(ITestOutputHelper outputHelper)
        {
            _output = outputHelper;
            _server = new ElasticsearchInside.Elasticsearch(c => c.EnableLogging().LogTo(this.WriteToLog));
        }

        private void WriteToLog(string message)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            _output.WriteLine(message);
        }

        private void WriteToLog(string format, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(format))
                return;

            _output.WriteLine(format, args);
        }

        public Uri ServerUrl
        {
            get { return _server.Url; }
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
                settings.DefaultIndex(this.DefaultIndex);
                return _client = new ElasticClient(settings);
            }
        }

        public string DefaultIndex {  get { return _index; } }

        protected TCmdLet CreateCmdLet<TCmdLet>() where TCmdLet : ElasticCmdlet, new()
        {
            var ret = new TCmdLet();
            ret.Node = this.Node;
            return ret;
        }

        protected virtual void DisposeInternal()
        {
        }

        protected virtual void Init()
        {
        }

        protected void CheckResponse(IResponse response)
        {
            var exception = response.OriginalException;
            if (!response.IsValid)
                throw exception ?? new Exception("Request failed");
        }

        /// <summary>
        /// Force elasticsearch to refresh index
        /// https://www.elastic.co/guide/en/elasticsearch/guide/current/near-real-time.html
        /// </summary>
        protected void RefreshIndex()
        {
            this.Client.Refresh(this.DefaultIndex);
        }

#if ESV2
        public Task InitializeAsync()
        {
            return Task.Run(() => this.Init());
        }
#else
        public async Task InitializeAsync()
        {
            await _server.Ready();
            await Task.Run(() => this.Init());
        }
#endif

        public Task DisposeAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    this.DisposeInternal();
                }
                finally
                {
                    _server.Dispose();
                }
            });
        }
    }
}
