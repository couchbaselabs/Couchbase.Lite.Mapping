using System;

namespace Couchbase.Lite.Mapping.Tests.TestObjects
{
    public class PropertyNameTestObject
    {
        [MappingPropertyName("STRingValuE")]
        public string StringValue { get; set; }
    }
}
