using System.Collections.Generic;

namespace Couchbase.Lite.Mapping.Tests.TestObjects
{
    public class SimpleObjectWithList : SimpleObject
    {
        public List<string> ListValue { get; set; }
    }
}
