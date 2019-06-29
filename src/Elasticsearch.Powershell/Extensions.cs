using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;

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

        public static PSObject ReflectToPSObject<T>(this T obj)
        {
            if (obj == null)
                return null;

            var record = new PSObject();

            foreach (var field in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                record.Properties.Add(new PSNoteProperty(field.Name, field.GetValue(obj)));

            return record;
        }
    }
}
