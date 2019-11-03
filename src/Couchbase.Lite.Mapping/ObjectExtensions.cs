using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Couchbase.Lite.Mapping;
using Newtonsoft.Json;

namespace Couchbase.Lite
{
    public static class ObjectExtensions
    {
        public static MutableDocument ToMutableDocument<T>(this T obj,
                                                           IPropertyNameConverter propertyNameConverter)
        {
            return ToMutableDocument(obj, null, propertyNameConverter);
        }

        public static MutableDocument ToMutableDocument<T>(this T obj, 
                                                           string id = null, 
                                                           IPropertyNameConverter propertyNameConverter = null)
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

            var dictionary = GetDictionary(obj, propertyNameConverter);

            if (dictionary != null)
            {
                document.SetData(dictionary);
            }

            return document;
        }

        static Dictionary<string, object> GetDictionary(object obj, IPropertyNameConverter propertyNameConverter = null)
        {
            var dictionary = new Dictionary<string, object>();

            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(pi => !Attribute.IsDefined(pi, typeof(JsonIgnoreAttribute))).ToList();

            foreach (PropertyInfo propertyInfo in properties)
            {
                string propertyName;

                var propertyValue = propertyInfo.GetValue(obj);
                var propertyType = propertyInfo.PropertyType;

                if (propertyType.IsEnum)
                {
                    var attribute = propertyInfo.PropertyType.GetMember(propertyValue.ToString()).FirstOrDefault()?.GetCustomAttribute<EnumMemberAttribute>();
                    if (attribute != null)
                    {
                        propertyValue = attribute.Value;
                    }
                }

                if (!propertyValue.IsNullOrDefault(propertyType))
                {
                    if (propertyInfo.CustomAttributes?.Count() > 0 && 
                        propertyInfo.GetCustomAttribute(typeof(MappingPropertyName)) is MappingPropertyName mappingProperty)
                    {
                        propertyName = mappingProperty.Name;
                    }
                    else if (propertyInfo.CustomAttributes?.Count() > 0 &&
                        propertyInfo.GetCustomAttribute(typeof(JsonPropertyAttribute)) is JsonPropertyAttribute jsonProperty)
                    {
                        propertyName = jsonProperty.PropertyName;
                    }
                    else if (propertyNameConverter != null)
                    {
                        propertyName = propertyNameConverter.Convert(propertyInfo.Name);
                    }
                    else
                    {
                        propertyName = Settings.PropertyNameConverter.Convert(propertyInfo.Name);
                    }

                    AddDictionaryValue(ref dictionary, propertyName, propertyValue, propertyInfo.PropertyType, propertyNameConverter);
                }
            }

            return dictionary;
        }

        static void AddDictionaryValue(ref Dictionary<string, object> dictionary, 
                                       string propertyName,
                                       object propertyValue, 
                                       Type propertyType,
                                       IPropertyNameConverter propertyNameConverter = null)
        {
            if (propertyType == typeof(byte[]) || propertyType == typeof(Stream))
            {
                dictionary[propertyName] = new Blob(string.Empty, (byte[])propertyValue);
            }
            else if (propertyType == typeof(DateTime))
            {
                dictionary[propertyName] = new DateTimeOffset((DateTime)propertyValue);
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
                            dictionaries.Add(GetDictionary(item, propertyNameConverter));
                        }

                        dictionary[propertyName] = dictionaries.ToArray();
                    }
                }
                else
                {
                    dictionary[propertyName] = GetDictionary(propertyValue, propertyNameConverter);
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
