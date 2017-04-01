using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Elasticsearch.Powershell.Tests
{
    public class RepositorySnapshotTests : ElasticTest
    {
        private string _shareDiskPath;
        private string _shareName;
        private string _shareNetworkPath;

        public RepositorySnapshotTests(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Init()
        {
            _shareDiskPath = Path.GetTempPath();
            _shareName = Guid.NewGuid().ToString();
            _shareNetworkPath = $"\\\\{Environment.MachineName}\\{_shareName}";

            _output.WriteLine($"Creating samba share '{_shareNetworkPath}' for folder '{_shareDiskPath}'");
            SambaShareApi.CreateShare(_shareDiskPath, _shareName, "", false, SharePermissions.All);
        }

        //[Fact]
        public void CreateRepository()
        {

        }
    }
}
