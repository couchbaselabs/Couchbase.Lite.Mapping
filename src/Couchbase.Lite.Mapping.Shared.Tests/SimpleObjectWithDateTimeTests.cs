using System;
using Xunit;

namespace Couchbase.Lite.Mapping.Tests
{
    public class SimpleObjectWithDateTimeTests
    {
        [Fact]
        public void TestWithDateTimeToMutableDocument()
        {
            var simpleObject = new TestObjects.SimpleObjectWithDateTime
            {
                StringValue = "Test Value",
                DateTimeValue = new DateTime(1985, 1, 8)
            };

            var mutableDocument = simpleObject.ToMutableDocument();
        }

        [Fact]
        public void TestWithDateTimeOffsetToMutableDocument()
        {
            var simpleObject = new TestObjects.SimpleObjectWithDateTime
            {
                StringValue = "Test Value",
                DateTimeOffsetValue = new DateTimeOffset(1985, 1, 8, 0, 0, 0, new TimeSpan(2,0,0))
            };

            var mutableDocument = simpleObject.ToMutableDocument();
        }

        [Fact]
        public void TestWithDateTimeAndDateTimeOffsetToMutableDocument()
        {
            var simpleObject = new TestObjects.SimpleObjectWithDateTime
            {
                StringValue = "Test Value",
                DateTimeValue = new DateTime(1985, 1, 8),
                DateTimeOffsetValue = new DateTimeOffset(1985, 1, 8, 0, 0, 0, new TimeSpan(2, 0, 0))
            };

            var mutableDocument = simpleObject.ToMutableDocument();
        }

        /*
        [Fact]
        public void TestWithDateTimeToMutableDocumentValueCheck()
        {
            var simpleObject = new TestObjects.SimpleObjectWithDateTime
            {
                StringValue = "Test Value",
                DateTimeValue = new DateTime(1985, 1, 8)
            };

            var mutableDocument = simpleObject.ToMutableDocument();

            Assert.Equal(mutableDocument.GetDate("dateTimeValue"), new DateTimeOffset(1985, 1, 8, 0, 0, 0, new TimeSpan()));
        }
        */
    }
}
