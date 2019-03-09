using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Couchbase.Lite
{
    public static class ObjectExtensions
    {
        public static MutableDocument ToMutableDocument<T>(this T obj, string id = null)
        {
            MutableDocument document;

            if (id != null)
            {
                document = new MutableDocument(id);
            }
            else
            {
                document = new MutableDocument();
            }

            var dictionary = GetDictionary(obj);

            if (dictionary != null)
            {
                document.SetData(dictionary);
            }

            return document;
        }

        static Dictionary<string, object> GetDictionary(object obj)
        {
            var dictionary = new Dictionary<string, object>();

            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var propertyName = propertyInfo.Name;
                var propertyValue = propertyInfo.GetValue(obj);
                var propertyType = propertyInfo.PropertyType;

                if (!propertyValue.IsNullOrDefault(propertyType))
                {
                    AddDictionaryValue(ref dictionary, propertyName, propertyValue, propertyInfo.PropertyType);
                };
            }

            return dictionary;
        }

        static void AddDictionaryValue(ref Dictionary<string, object> dictionary, string propertyName,
                                            object propertyValue, Type propertyType)
        {
            if (propertyType == typeof(byte[]) || propertyType == typeof(Stream))
            {
                dictionary[propertyName] = new Blob(string.Empty, (byte[])propertyValue);
            }
            else if (!propertyType.IsSimple() && !propertyType.IsEnum && propertyType.IsClass && propertyValue != null)
            {
                if (typeof(IEnumerable).IsAssignableFrom(propertyType))
                {
                    if (propertyType.IsArray && propertyType.GetElementType().IsSimple()
                         || (!propertyType.IsArray && propertyValue is IList 
                             && propertyValue.GetType().GetTypeInfo().GenericTypeArguments[0].IsSimple()))
                    {
                        dictionary[propertyName] = propertyValue;
                    }
                    else
                    {
                        var items = propertyValue as IEnumerable;

                        var dictionaries = new List<Dictionary<string, object>>();

                        foreach (var item in items)
                        {
                            dictionaries.Add(GetDictionary(item));
                        }

                        dictionary[propertyName] = dictionaries.ToArray();
                    }
                }
                else
                {
                    dictionary[propertyName] = GetDictionary(propertyValue);
                }
            }
            else if (propertyType.IsEnum)
            {
                dictionary[propertyName] = propertyValue.ToString();
            }
            else
            {
                dictionary[propertyName] = propertyValue;
            }
        }

        internal static bool IsNullOrDefault(this object obj, Type runtimeType)
        {
            if (obj == null) return true;

            // Handle non-null reference types.
            if (!runtimeType.IsValueType) return false;

            // Nullable, but not null
            if (Nullable.GetUnderlyingType(runtimeType) != null) return false;

            // Use CreateInstance as the most reliable way to get default value for a value type
            object defaultValue = Activator.CreateInstance(runtimeType);

            return defaultValue.Equals(obj);
        }

        static bool IsSimple(this Type type) => (type.IsValueType || type == typeof(string));
    }
}
