namespace Couchbase.Lite.Mapping.Tests.TestObjects
{
    public class SimpleObjectWithEnum
    {
        [MappingPropertyName("SimpleEnum")]
        public SimpleEnum SimpleEnum { get; set; }
    }
}
