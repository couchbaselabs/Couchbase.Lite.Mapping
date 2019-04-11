namespace Couchbase.Lite.Mapping
{
    public static class Settings
    {
        static IPropertyNameConverter _propertyNameConverter;
        public static IPropertyNameConverter PropertyNameConverter
        {
            get
            {
                if (_propertyNameConverter == null)
                {
                    _propertyNameConverter = new LowerCamelCasePropertyNameConverter();
                }

                return _propertyNameConverter;
            }
            set => _propertyNameConverter = value;
        }
    }
}
