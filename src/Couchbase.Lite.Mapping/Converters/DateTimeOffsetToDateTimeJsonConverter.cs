using System;
using Newtonsoft.Json;

namespace Couchbase.Lite.Mapping
{
    public class DateTimeOffsetToDateTimeJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(DateTimeOffset).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var newValue = ((DateTimeOffset)value).DateTime;
            serializer.Serialize(writer, newValue);
        }
    }
}
