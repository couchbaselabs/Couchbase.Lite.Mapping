using Xunit;

namespace Couchbase.Lite.Mapping.Tests
{
    public class SimpleObjectTests
    {
        [Fact]
        public void TestToMutableDocument()
        {
            var simpleObject = new TestObjects.SimpleObject
            {
                StringValue = "Test Value"
            };

            var mutableDocument = simpleObject.ToMutableDocument();
        }
    }
}
