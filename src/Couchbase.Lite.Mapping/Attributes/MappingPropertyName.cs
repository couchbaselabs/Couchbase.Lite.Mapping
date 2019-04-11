using System;

namespace Couchbase.Lite.Mapping
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MappingPropertyName : Attribute
    {
        public string Name { get; private set; }

        public MappingPropertyName(string name)
        {
            Name = name;
        }
    }
}
