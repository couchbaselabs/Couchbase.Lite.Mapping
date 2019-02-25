using System;
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

        public static MutableDocument ToMutableDocument<T>(this T obj) where T : DocumentObject
        {
            var document = new MutableDocument(obj.Id);

            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var propertyName = propertyInfo.Name;
                var propertyValue = propertyInfo.GetValue(obj);

                if (propertyValue != null && propertyName.ToLower() != "id")
                {
                    AddDocumentValue(ref document, propertyName, propertyValue, propertyInfo.PropertyType);
                }
            }

            return document;
        }

        static void AddDocumentValue(ref MutableDocument document, string propertyName, object propertyValue, Type propertyType)
        {
            try
            {
                if (propertyType == typeof(int))
                {
                    document.SetInt(propertyName, (int)propertyValue);
                }
                else if (propertyType == typeof(Blob))
                {
                    document.SetBlob(propertyName, (Blob)propertyValue);
                }
                else if (propertyType == typeof(Dictionary<string,object>))
                {
                    document.SetData((Dictionary<string, object>)propertyValue);
                }
                else if (propertyType == typeof(DictionaryObject))
                {
                    document.SetDictionary(propertyName, (DictionaryObject)propertyValue);
                }
                else if (propertyType == typeof(DateTime))
                {
                    document.SetDate(propertyName, (DateTime)propertyValue);
                }
                else if (propertyType == typeof(long))
                {
                    document.SetLong(propertyName, (long)propertyValue);
                }
                else if (propertyType == typeof(ArrayObject))
                {
                    document.SetArray(propertyName, (ArrayObject)propertyValue);
                }
                else if (propertyType == typeof(float))
                {
                    document.SetFloat(propertyName, (float)propertyValue);
                }
                else if (propertyType == typeof(object))
                {
                    document.SetValue(propertyName, propertyValue);
                }
                else if (propertyType == typeof(double))
                {
                    document.SetDouble(propertyName, (double)propertyValue);
                }
                else if (propertyType == typeof(string))
                {
                    document.SetString(propertyName, propertyValue.ToString());
                }
                else if (propertyType == typeof(bool))
                {
                    document.SetBoolean(propertyName, (bool)propertyValue);
                }
                else
                {
                    throw new Exception("Couchbase.Lite.Mapping Exception - Type not supported!");
                }
            }
            catch(Exception ex)
            {
                // TODO: Log?
                throw ex;
            }
        }
    }
}
