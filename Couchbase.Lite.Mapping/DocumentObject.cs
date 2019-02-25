using Newtonsoft.Json;

namespace Couchbase.Lite.Mapping
{
    public abstract class DocumentObject
    {
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }
    }
}
