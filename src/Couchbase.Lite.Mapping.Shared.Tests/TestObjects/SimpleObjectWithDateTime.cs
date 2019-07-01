using System;

namespace Couchbase.Lite.Mapping.Tests.TestObjects
{
    public class SimpleObjectWithDateTime : SimpleObject
    {
        public DateTime DateTimeValue { get; set; }
        public DateTimeOffset DateTimeOffsetValue { get; set; }
    }
}
