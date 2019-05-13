using System.Collections.Generic;
using System.Linq;
using Couchbase.Lite.Mapping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Couchbase.Lite
{
    public static class ResultSetExtensions
    {
        public static IEnumerable<T> ToObjects<T>(this List<Query.Result> results, string target)
        {
            List<T> objects = default(List<T>);

            if (results?.Select(x => x?.GetDictionary(target)?.ToDictionary()) is IEnumerable<Dictionary<string, object>> dictionaries)
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new ExcludeStreamPropertiesResolver()
                };

                settings.Converters?.Add(new BlobToBytesJsonConverter());

                objects = new List<T>();

                // TODO: Replace with serializing and parsing the List<Dictionary> directly
                foreach (var dictionary in dictionaries)
                {
                    var json = JsonConvert.SerializeObject(dictionary, settings);

                    if (!string.IsNullOrEmpty(json))
                    {
                        var jObj = JObject.Parse(json);

                        if (jObj != null)
                        {
                            objects.Add(jObj.ToObject<T>());
                        }
                    }
                }
            }

            return objects;
        }
    }
}
