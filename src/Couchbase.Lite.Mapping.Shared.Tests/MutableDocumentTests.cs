using System.Collections.Generic;
using Xunit;

namespace Couchbase.Lite.Mapping.Tests
{
    public class MutableDocumentTests
    {
        [Fact]
        public void TestToSimpleObject()
        {
            var dictionary = new Dictionary<string, object>
            {
                ["StringValue"] = "Test Value"
            };

            var mutableDocument = new MutableDocument("test_id",dictionary);

            var simpleObject = mutableDocument.ToObject<TestObjects.SimpleObject>();
        }

        [Fact]
        public void TestToSimpleObjectWithSimpleArray()
        {
            var dictionary = new Dictionary<string, object>
            {
                ["StringValue"] = "Test Value",
                ["ArrayValue"] = new string[] { "Value1", "Value2" }
            };

            var mutableDocument = new MutableDocument("test_id", dictionary);

            var simpleObject = mutableDocument.ToObject<TestObjects.SimpleObject>();
        }

        [Fact]
        public void TestToSimpleObjectWithSimpleList()
        {
            var dictionary = new Dictionary<string, object>
            {
                ["StringValue"] = "Test Value",
                ["ListValue"] = new List<string> { "Value1", "Value2" }
            };

            var mutableDocument = new MutableDocument("test_id", dictionary);

            var simpleObject = mutableDocument.ToObject<TestObjects.SimpleObject>();
        }

        /*
        [Fact]
        public void TestToNestedSimpleObject()
        {
            var dictionary = new Dictionary<string, object>
            {
                ["StringValue"] = "Test Value",
                ["ChildSimpleObject"] = new Dictionary<string,object>
                {
                    ["StringValue"] = "Child Test Value"
                }
            };

            var mutableDocument = new MutableDocument("test_id", dictionary);

            var simpleObject = mutableDocument.ToObject<TestObjects.NestedSimpleObject>();
        }
        */
    }
}
