using Xunit;

namespace Couchbase.Lite.Mapping.Tests
{
    public class SimpleObjectWithListTests
    {
        [Fact]
        public void TestToMutableDocument()
        {
            var simpleObject = new TestObjects.SimpleObjectWithArray
            {
                StringValue = "Test Value",
                ArrayValue = new string[] { "Value 1", "Value 2" }
            };

            var mutableDocument = simpleObject.ToMutableDocument();
        }
    }
}
