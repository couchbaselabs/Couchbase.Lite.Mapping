﻿using System;
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

            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(pi => !Attribute.IsDefined(pi, typeof(JsonIgnoreAttribute)))?.ToList();

            foreach (PropertyInfo propertyInfo in properties)
            {
                string propertyName;

                var propertyValue = propertyInfo.GetValue(obj);
                var propertyType = propertyInfo.PropertyType;

                if (propertyValue != null)
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
            else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?)) {
                var dateTimeVal = ((DateTime) propertyValue);
                if (dateTimeVal != default(DateTime)) {
                    dictionary[propertyName] = new DateTimeOffset((DateTime) propertyValue);
                }
            }
            else if (!propertyType.IsSimple() && !propertyType.IsEnum && propertyType.IsClass && propertyValue != null)
            {
                if (typeof(IEnumerable).IsAssignableFrom(propertyType))
                {
                    if (propertyType.IsArray && propertyType.GetElementType().IsEnum
                         || (!propertyType.IsArray && propertyValue is IList
                             && propertyValue.GetType().GetTypeInfo().GenericTypeArguments[0].IsEnum))
                    {
                        Type enumType = propertyType.IsArray ?
                                            propertyType.GetElementType() :
                                            propertyValue.GetType().GetTypeInfo().GenericTypeArguments[0];

                        var propertyValueEnumList = new List<string>();
                        foreach (object @object in propertyValue as IEnumerable)
                        {
                            EnumMemberAttribute itemAttribute = enumType.GetMember(@object.ToString()).FirstOrDefault()?.GetCustomAttribute<EnumMemberAttribute>();
                            if (itemAttribute != null)
                            {
                                propertyValueEnumList.Add(itemAttribute.Value);
                            }
                        }
                        dictionary[propertyName] = propertyValueEnumList;
                    }

                    else if (propertyType.IsArray && propertyType.GetElementType().IsSimple()
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
                var attribute = propertyType.GetMember(propertyValue.ToString()).FirstOrDefault()?.GetCustomAttribute<EnumMemberAttribute>();
                if (attribute != null)
                {
                    propertyValue = attribute.Value;
                }
                dictionary[propertyName] = propertyValue.ToString();
            }
            else
            {
                dictionary[propertyName] = propertyValue;
            }
        }

        static bool IsSimple(this Type type) => (type.IsValueType || type == typeof(string));
    }
}
