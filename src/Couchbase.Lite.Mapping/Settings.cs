namespace Couchbase.Lite.Mapping
{
    /// <summary>
    /// Global settings
    /// </summary>
    public static class Settings
    {
        static IPropertyNameConverter _propertyNameConverter;

        /// <summary>
        /// Set a global PropertyNameConverter
        /// </summary>
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
