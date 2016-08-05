using System;
using Nest;
using Xunit.Abstractions;

namespace Elasticsearch.Powershell.Tests
{
    public class ElasticTest
    {
        protected readonly ITestOutputHelper Output;

        protected ElasticTest(ITestOutputHelper outputHelper)
        {
            this.Output = outputHelper;
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
