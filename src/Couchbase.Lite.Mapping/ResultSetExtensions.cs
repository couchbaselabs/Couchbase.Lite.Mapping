using System;
using System.Collections.Generic;
using Couchbase.Lite.Mapping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Couchbase.Lite
{
    public static class ResultSetExtensions
    {
        public static T ToObject<T>(this Query.Result result)
        {
            T obj = default;

            if (result != null)
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new ExcludeStreamPropertiesResolver()
                };

                settings.Converters?.Add(new BlobToBytesJsonConverter());

                JObject rootJObj = new JObject();

                foreach (var key in result.Keys)
                {
                    var value = result[key]?.Value;

                    if (value != null)
                    {
                        JObject jObj = null;

                        if (value.GetType() == typeof(DictionaryObject))
                        {
                            var json = JsonConvert.SerializeObject(value, settings);

                            if (!string.IsNullOrEmpty(json))
                            {
                                jObj = JObject.Parse(json);
                            }
                        }
                        else
                        {
                            jObj = new JObject
                            {
                                new JProperty(key, value)
                            };
                        }

                        if (jObj != null)
                        {
                            rootJObj.Merge(jObj, new JsonMergeSettings
                            {
                                // Union array values together to avoid duplicates (e.g. "id")
                                MergeArrayHandling = MergeArrayHandling.Union
                            });
                        }

                        if (rootJObj != null)
                        {
                            obj = rootJObj.ToObject<T>();
                        }
                    }
                }
            }

            return obj;
        }

        public static IEnumerable<T> ToObjects<T>(this List<Query.Result> results)
        {
            List<T> objects = default;

            if (results?.Count > 0)
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new ExcludeStreamPropertiesResolver()
                };

                settings.Converters?.Add(new BlobToBytesJsonConverter());

                objects = new List<T>();

                foreach (var result in results)
                {
                    var obj = ToObject<T>(result);

                    if (!EqualityComparer<T>.Default.Equals(obj, default(T)))                   
                    {
                        objects.Add(obj);
                    }
                }
            }

            return objects;
        }
    }
}
