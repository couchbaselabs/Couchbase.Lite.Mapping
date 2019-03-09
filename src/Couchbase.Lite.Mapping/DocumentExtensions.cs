using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Couchbase.Lite
{
    public static class DocumentExtensions
    {
        public static T ToObject<T>(this Document document)
        {
            T obj = default(T);

            try
            {
                if (document != null)
                {
                    if (document.ToDictionary()?.Count > 0)
                    {
                        var settings = new JsonSerializerSettings
                        {
                            ContractResolver = new ExcludeStreamPropertiesResolver()
                        };

                        settings.Converters.Add(new BlobToBytesJsonConverter());

                        var json = JsonConvert.SerializeObject(document.ToMutable().ToDictionary(), settings);

                        var jObj = JObject.Parse(json);

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
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Couchbase.Lite.Mapper - Error: {ex.Message}");
            }

            return obj;
        }
    }
}
