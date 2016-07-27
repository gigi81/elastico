using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Powershell
{
    internal static class Extensions
    {
        public static PSObject ToPSObject(this IDictionary<string, object> dictionary)
        {
            var record = new PSObject();

            foreach (var field in dictionary)
            {
                record.Properties.Add(new PSNoteProperty(field.Key, field.Value));
            }

            return record;
        }

        public static long ParseLong(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return 0;

            return long.Parse(value);
        }
    }
}
