namespace Couchbase.Lite.Mapping
{
    public sealed class LowerCamelCasePropertyNameConverter : IPropertyNameConverter
    {
        public string Convert(string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return val;
            }

            if (val.Length > 1)
            {
                return char.ToLowerInvariant(val[0]) + val.Substring(1);
            }

            return val.ToLower();
        }
    }
}
