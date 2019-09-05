using System.Collections.Generic;
using Couchbase.Lite.Mapping.Tests.TestObjects;
using System;
using Xunit;

namespace Couchbase.Lite.Mapping.Tests
{
    public class SimpleEnumTests
    {
        [Fact]
        public void TestToSimpleEnum()
        {
            var dictionary = new Dictionary<string, object>
            {
                ["SimpleEnum"] = "EnumValue_2",
            };

            var mutableDocument = new MutableDocument("test_id", dictionary);

            var simpleObject = mutableDocument.ToObject<SimpleObjectWithEnum>();

            Assert.Equal(SimpleEnum.Enum_2, simpleObject.SimpleEnum);
        }
    }
}
