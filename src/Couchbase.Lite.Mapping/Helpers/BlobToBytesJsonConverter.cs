using System;
using Newtonsoft.Json;

namespace Couchbase.Lite
{
    public class BlobToBytesJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Blob).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var bytes = (value as Blob)?.Content;

            if (bytes != null)
            {
                serializer.Serialize(writer, Convert.ToBase64String(bytes));
            }
        }
    }
}
