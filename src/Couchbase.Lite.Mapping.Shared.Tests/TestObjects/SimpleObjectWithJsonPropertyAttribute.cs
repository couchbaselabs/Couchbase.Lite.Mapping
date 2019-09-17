using Newtonsoft.Json;

namespace Couchbase.Lite.Mapping.Shared.Tests.TestObjects
{
    public class SimpleObjectWithJsonPropertyAttribute
    {
        [JsonProperty("JsonStringProperty")]
        public string StringValue { get; set; }
    }
}
