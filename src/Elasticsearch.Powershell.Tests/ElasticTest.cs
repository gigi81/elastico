using System;
using Nest;
using Xunit.Abstractions;

namespace Elasticsearch.Powershell.Tests
{
    public class ElasticTest : IDisposable
    {
        protected readonly ITestOutputHelper Output;
        private readonly ElasticsearchInside.Elasticsearch Server;

        protected ElasticTest(ITestOutputHelper outputHelper)
        {
            this.Output = outputHelper;
            this.Server = new ElasticsearchInside.Elasticsearch();
        }

        public Uri ServerUrl
        {
            get { return this.Server.Url; }
        }

        public string[] Node
        {
            get { return new[] { this.ServerUrl.ToString() }; }
        }

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

        protected virtual void DisposeInternal()
        {
        }

        protected void CheckResponse(IResponse response)
        {
#if ESV1
            if (!response.IsValid)
                throw response.RequestInformation.OriginalException;
#else
            if (!response.IsValid)
                throw response.OriginalException;
#endif
        }
    }
}
