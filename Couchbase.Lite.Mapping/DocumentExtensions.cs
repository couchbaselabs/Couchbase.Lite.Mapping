using System;
using System.Reflection;
using Couchbase.Lite.Mapping;

namespace Couchbase.Lite
{
    public static class DocumentExtensions
    {
        public static T ToObject<T>(this Document document) where T : DocumentObject
        {
            T obj = default(T);

            if (document != null)
            {
                obj = Activator.CreateInstance<T>();

                obj.Id = document.Id;

                var items = document.ToDictionary();

                foreach (var item in items)
                {
                    var t = item.GetType();

                    Type type = obj.GetType();

                    PropertyInfo prop = type.GetProperty(item.Key);

                    prop.SetValue(obj, item.Value, null);
                }
            }

            return obj;
        }

        public static MutableDocument ToMutableDocument<T>(this T obj) where T : DocumentObject
        {
            var document = new MutableDocument(obj.Id);

            foreach (PropertyInfo property in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var val = property.GetValue(obj);
                var t = property.GetType();
                var name = property.Name;

                if (val != null && name.ToLower() != "id")
                {
                    document.SetString(name, val.ToString());
                }
            }

            return document;
        }
    }
}
