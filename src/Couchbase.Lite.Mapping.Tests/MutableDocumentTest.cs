using System.Collections.Generic;
using Xunit;

namespace Couchbase.Lite.Mapping.Tests
{
    public class MutableDocumentTest
    {
        [Fact]
        public void TestToSimpleObject()
        {
            var dictionary = new Dictionary<string, object>
            {
                ["StringValue"] = "Test Value"
            };

            var mutableDocument = new MutableDocument("test",dictionary);

            var simpleObject = mutableDocument.ToObject<TestObjects.SimpleObject>();
        }
    }
}
