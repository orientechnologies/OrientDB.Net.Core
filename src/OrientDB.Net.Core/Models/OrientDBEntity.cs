using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OrientDB.Net.Core.Models
{
    public abstract class OrientDBEntity
    {
        public ORID ORID { get; set; }
        public int OVersion { get; set; }
        public short OClassId { get; set; }
        public string OClassName { get; set; }

        public virtual void Hydrate(IDictionary<string, object> data)
        {
            var type = this.GetType();

            foreach (var key in data.Keys)
            {
                var property = type.GetProperty(key);
                if (property == null || !property.CanWrite) continue;

                var propertyType = property.PropertyType;
                if (data[key] == null)
                {
                    property.SetValue(this, null);
                }
                else if (data[key].GetType().GetInterfaces().Any(n => n == typeof(IConvertible)))
                {
                    object val = Convert.ChangeType(data[key], propertyType);
                    property.SetValue(this, val);
                }
                else
                {
                    var objectType = property.PropertyType;

                    if (objectType.Name == typeof(HashSet<>).Name || objectType.Name == typeof(List<>).Name)
                    {
                        ExtractList(data, objectType, key, property);
                        continue;
                    }

                    if (objectType.Name == typeof(Dictionary<,>).Name)
                    {
                        ExtractDictionary(data, objectType, key, property);
                    }
                }
            }
        }

        private void ExtractList(IDictionary<string, object> data, Type objectType, string key,
            PropertyInfo property)
        {
            var list = Activator.CreateInstance(objectType);

            var enumerable = data[key] as IEnumerable;
            if (enumerable != null)
            {
                foreach (var item in enumerable)
                {
                    objectType.GetMethod("Add").Invoke(list, new[] { Convert.ChangeType(item, objectType.GenericTypeArguments.First()) });
                }
            }

            property.SetValue(this, list);

        }

        private void ExtractDictionary(IDictionary<string, object> data, Type objectType, string key, PropertyInfo property)
        {
            var dictionary = Activator.CreateInstance(objectType);

            var enumerable = data[key] as IDictionary<string, object>;
            if (enumerable != null)
            {
                foreach (var item in enumerable)
                {
                    objectType.GetMethod("Add").Invoke(dictionary, new[] { item.Key, item.Value });
                }
            }

            property.SetValue(this, dictionary);
        }
    }
}
