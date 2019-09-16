using System.Runtime.Serialization;

namespace Couchbase.Lite.Mapping.Tests.TestObjects
{
	public enum SimpleEnum
	{
		[EnumMember(Value = "EnumValue_1")]
		Enum_1,
		[EnumMember(Value = "EnumValue_2")]
		Enum_2
	}
}
