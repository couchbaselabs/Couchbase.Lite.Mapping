using System.Collections.Generic;

namespace Couchbase.Lite.Mapping.Tests.TestObjects
{ 
	public class SimpleObjectWithEnumerableEnum
    {
        [MappingPropertyName("ArraySimpleEnums")]
        public SimpleEnum[] ArraySimpleEnums { get; set; }

        [MappingPropertyName("ListSimpleEnums")]
        public List<SimpleEnum> ListSimpleEnums { get; set; }
    }
}
