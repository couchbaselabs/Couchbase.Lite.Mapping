using Couchbase.Lite.Mapping;
using Newtonsoft.Json;

namespace UserProfileDemo.Core.Models
{
    public abstract class BaseModel : DocumentObject
    {
        [JsonProperty("type")]
        public abstract string Type { get; set; }
    }
}
