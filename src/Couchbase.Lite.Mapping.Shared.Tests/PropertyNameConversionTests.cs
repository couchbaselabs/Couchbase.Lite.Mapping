using System;
using Xunit;
using Couchbase.Lite.Mapping.Tests.TestObjects;
using System.Collections.Generic;

namespace Couchbase.Lite.Mapping.Tests
{
    public class PropertyNameConversionTests
    {
        [Fact]
        public void TestDefaultPropertyNameConverterToMutableDocument()
        {
            var simpleObject = new SimpleObject
            {
                StringValue = "Test Value"
            };

            var mutableDocument = simpleObject.ToMutableDocument();

            var result = mutableDocument?.Keys.Contains("stringValue");

            Assert.True(result, "Property Name is not getting converted correctly.");
        }

        [Fact]
        public void TestCustomGlobalPropertyNameConverterToMutableDocument()
        {
            var simpleObject = new SimpleObject
            {
                StringValue = "Test Value"
            };

            Settings.PropertyNameConverter = new CustomPropertyNameConverter();

            var mutableDocument = simpleObject.ToMutableDocument();

            var result = mutableDocument?.Keys.Contains("StringValue?");

            Assert.True(result, "Property Name is not getting converted correctly.");
        }

        [Fact]
        public void TestCustomDocumentPropertyNameConverterToMutableDocument()
        {
            var simpleObject = new SimpleObject
            {
                StringValue = "Test Value"
            };

            var mutableDocument = simpleObject.ToMutableDocument(new CustomPropertyNameConverter());

            var result = mutableDocument?.Keys.Contains("StringValue?");

            Assert.True(result, "Property Name is not getting converted correctly.");
        }

        [Fact]
        public void TestCustomAttributePropertyNameConverterToMutableDocument()
        {
            var simpleObject = new PropertyNameTestObject
            {
                StringValue = "Test Value"
            };

            var mutableDocument = simpleObject.ToMutableDocument();

            var result = mutableDocument?.Keys.Contains("STRingValuE");

            Assert.True(result, "Property Name is not getting converted correctly.");
        }

        [Fact]
        public void TestEnumToMutableDocument()
        {
            var simpleObject = new SimpleObjectWithEnum
            {
                SimpleEnum = SimpleEnum.Enum_2
            };

            var mutableDocument = simpleObject.ToMutableDocument();

            var result = mutableDocument.Keys.Contains("SimpleEnum");
            var enumValue = mutableDocument.GetString("SimpleEnum");

            Assert.True(result, "Property Name is not getting converted correctly.");
            Assert.Equal("EnumValue_2", enumValue);
        }

        [Fact]
        public void TestArrayEnumToMutableDocument()
        {
            var simpleObject = new SimpleObjectWithEnumerableEnum
            {
                ArraySimpleEnums = new[] { SimpleEnum.Enum_1, SimpleEnum.Enum_2 }
            };

            var mutableDocument = simpleObject.ToMutableDocument();

            var result = mutableDocument.Keys.Contains("ArraySimpleEnums");
            var enums = mutableDocument.GetArray("ArraySimpleEnums");
            var enumValue1 = enums.GetString(0);
            var enumValue2 = enums.GetString(1);

            Assert.True(result, "Property Name is not getting converted correctly.");
            Assert.Equal("EnumValue_1", enumValue1);
            Assert.Equal("EnumValue_2", enumValue2);
        }

        [Fact]
        public void TestListEnumToMutableDocument()
        {
            var simpleObject = new SimpleObjectWithEnumerableEnum
            {
                ListSimpleEnums = new List<SimpleEnum> { SimpleEnum.Enum_2, SimpleEnum.Enum_3 }
            };

            var mutableDocument = simpleObject.ToMutableDocument();

            var result = mutableDocument.Keys.Contains("ListSimpleEnums");
            var enums = mutableDocument.GetArray("ListSimpleEnums");
            var enumValue1 = enums.GetString(0);
            var enumValue2 = enums.GetString(1);

            Assert.True(result, "Property Name is not getting converted correctly.");
            Assert.Equal("EnumValue_2", enumValue1);
            Assert.Equal("EnumValue_3", enumValue2);
        }
    }

    public class CustomPropertyNameConverter : IPropertyNameConverter
    {
        public string Convert(string val) => val + "?";
    }
}
