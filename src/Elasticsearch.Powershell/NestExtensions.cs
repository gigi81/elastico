using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Elasticsearch.Powershell
{
    internal static class NestExtensions
    {
#if ESV2 || ESV5 || ESV6
        public static Types.Repository GetRepository(this IGetRepositoryResponse response, string name)
        {
            var repo = response.Repositories[name];

            return new Types.Repository
            {
                Name = name,
                Type = repo.Type,
                Settings = response.GetSettings(name)
            };
        }

        public static object GetSettings(this IGetRepositoryResponse response, string name)
        {
            switch (response.Repositories[name].Type.ToLowerInvariant())
            {
                case "fs":
                    return response.FileSystem(name).Settings;

                case "s3":
                    return response.S3(name).Settings;

                case "azure":
                    return response.Azure(name).Settings;

                case "hdfs":
                    return response.Hdfs(name).Settings;

                case "url":
                    return response.ReadOnlyUrl(name).Settings;

                default:
                    return null;
            }
        }
#else
        public static Types.Repository GetRepository(this GetRepositoryResponse response, string name)
        {
            var repo = response.Repositories[name];

            return new Types.Repository
            {
                Name = name,
                Type = repo.Type,
                Settings = response.GetSettings(name)
            };
        }

        public static object GetSettings(this GetRepositoryResponse response, string name)
        {
            switch (response.Repositories[name].Type.ToLowerInvariant())
            {
                case "fs":
                    return response.FileSystem(name).Settings;

                case "s3":
                    return response.S3(name).Settings;

                case "azure":
                    return response.Azure(name).Settings;

                case "hdfs":
                    return response.Hdfs(name).Settings;

                case "url":
                    return response.ReadOnlyUrl(name).Settings;

                default:
                    return null;
            }
        }
#endif

        public static Names ToNames(this string[] names)
        {
            if (names == null)
                throw new ArgumentNullException(nameof(names));

            if (names.Length == 0)
                throw new ArgumentException(nameof(names));

            return Names.Parse(String.Join(",", names.Where(n => !String.IsNullOrWhiteSpace(n))));
        }

        public static Names ToNames(this string[] names, string defaultName)
        {
            if (names == null || names.Length == 0)
                return new[] { defaultName }.ToNames();

            return names.ToNames();
        }
    }
}
