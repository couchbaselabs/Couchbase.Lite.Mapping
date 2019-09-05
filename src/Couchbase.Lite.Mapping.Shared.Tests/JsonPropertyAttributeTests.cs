using Couchbase.Lite.Mapping.Shared.Tests.TestObjects;
using Xunit;

namespace Couchbase.Lite.Mapping.Tests
{
    public class JsonPropertyAttributeTests
    {
        [Fact]
        public void TestJsonPropertyToMutableDocument()
        {
            var simpleObject = new SimpleObjectWithJsonPropertyAttribute
            {
                StringValue = "Test Value"
            };

            var mutableDocument = simpleObject.ToMutableDocument();
            var result = mutableDocument.Keys.Contains("JsonStringProperty");
            var value = mutableDocument.GetString("JsonStringProperty");

            Assert.True(result, "Property Name is not getting converted correctly.");
            Assert.Equal("Test Value", value);
        }
    }
}
