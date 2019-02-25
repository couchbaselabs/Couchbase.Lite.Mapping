using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Couchbase.Lite.Mapping;
using Newtonsoft.Json.Linq;

namespace Couchbase.Lite
{
    public static class DocumentExtensions
    {
        public static T ToObject<T>(this Document document) where T : DocumentObject
        {
            T obj = default(T);

            try
            {
                if (document != null)
                {
                    if (document.ToDictionary()?.Count > 0)
                    {
                        var jObj = JObject.FromObject(document.ToDictionary());

                        if (jObj != null)
                        {
                            obj = jObj.ToObject<T>();
                        }
                        else
                        {
                            obj = Activator.CreateInstance<T>();
                        }
                    }
                    else
                    {
                        obj = Activator.CreateInstance<T>();
                    }

                    obj.Id = document.Id;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Couchbase.Lite.Mapper - Error: {ex.Message}");
            }

            return obj;
        }

        public static MutableDocument ToMutableDocument<T>(this T obj, string id = null) where T : DocumentObject
        {
            MutableDocument document;

            if (id != null)
            {
                document = new MutableDocument(obj.Id);
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

                if (propertyValue != null && propertyName.ToLower() != "id")
                {
                    AddDictionaryValue(ref dictionary, propertyName, propertyValue, propertyInfo.PropertyType);
                }
            }

            return dictionary;
        }

        static void AddDictionaryValue(ref Dictionary<string,object> dictionary, string propertyName, 
                                            object propertyValue, Type propertyType)
        {
            if (propertyType != typeof(decimal) && propertyType != typeof(string) 
                 && !propertyType.IsEnum && propertyType.IsClass && propertyValue != null)
            {
                if (typeof(IEnumerable).IsAssignableFrom(propertyType))
                {
                    var items = propertyValue as IEnumerable;

                    var dictionaries = new List<Dictionary<string, object>>();

                    foreach (var item in items)
                    {
                        dictionaries.Add(GetDictionary(item));
                    }

                    dictionary[propertyName] = dictionaries.ToArray();
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
    }
}
