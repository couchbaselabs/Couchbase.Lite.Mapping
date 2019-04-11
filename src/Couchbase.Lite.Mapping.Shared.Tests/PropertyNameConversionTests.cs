using System;
using Xunit;
using Couchbase.Lite.Mapping.Tests.TestObjects;

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
    }

    public class CustomPropertyNameConverter : IPropertyNameConverter
    {
        public string Convert(string val) => val + "?";
    }
}
