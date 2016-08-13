using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Newtonsoft.Json;

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

        public static IDictionary<string, object> ToDictionary(this Hashtable table)
        {
            var dict = new Dictionary<string, object>();
            foreach (DictionaryEntry kvp in table)
                dict.Add((string)kvp.Key, kvp.Value);
            return dict;
        }

        public static TAttribute GetAttribute<TAttribute>(this MemberInfo property) where TAttribute : Attribute
        {
            return property.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
        }

#if ESV1
        public static TRequest SetJsonProperties<TRequest>(this TRequest request, IDictionary<string, object> values) where TRequest : IRequest
        {
            var properties = request.GetType().GetProperties();

            foreach (var property in properties)
            {
                var name = property.GetAttribute<JsonPropertyAttribute>()?.PropertyName;
                if (String.IsNullOrEmpty(name))
                    continue;


                object value;
                if (!values.TryGetValue(name, out value))
                    continue;

                property.SetValue(request, value);
            }

            return request;
        }
#endif
    }
}
