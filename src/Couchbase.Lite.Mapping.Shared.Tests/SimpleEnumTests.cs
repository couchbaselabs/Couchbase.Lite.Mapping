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

        [Fact]
        public void TestToSimpleArrayEnum()
        {
            var dictionary = new Dictionary<string, object>
            {
                ["ArraySimpleEnums"] = new[] { "EnumValue_1", "EnumValue_2" }
            };

            var mutableDocument = new MutableDocument("test_id", dictionary);

            var simpleObjectWithEnumerableEnum = mutableDocument.ToObject<SimpleObjectWithEnumerableEnum>();

            Assert.Equal(SimpleEnum.Enum_1, simpleObjectWithEnumerableEnum.ArraySimpleEnums[0]);
            Assert.Equal(SimpleEnum.Enum_2, simpleObjectWithEnumerableEnum.ArraySimpleEnums[1]);
        }

        [Fact]
        public void TestToSimpleListEnum()
        {
            var dictionary = new Dictionary<string, object>
            {
                ["ListSimpleEnums"] = new[] { "EnumValue_2", "EnumValue_3" }
            };

            var mutableDocument = new MutableDocument("test_id", dictionary);

            var simpleObjectWithEnumerableEnum = mutableDocument.ToObject<SimpleObjectWithEnumerableEnum>();

            Assert.Equal(SimpleEnum.Enum_2, simpleObjectWithEnumerableEnum.ListSimpleEnums[0]);
            Assert.Equal(SimpleEnum.Enum_3, simpleObjectWithEnumerableEnum.ListSimpleEnums[1]);
        }
    }
}
