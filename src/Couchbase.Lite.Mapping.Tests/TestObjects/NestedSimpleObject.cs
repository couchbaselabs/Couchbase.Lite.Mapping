namespace Couchbase.Lite.Mapping.Tests.TestObjects
{
    public class NestedSimpleObject : SimpleObject
    {
        public SimpleObject ChildSimpleObject { get; set; }
    }
}
