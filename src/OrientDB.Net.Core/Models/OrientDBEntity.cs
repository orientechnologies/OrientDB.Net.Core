using OrientDB.Net.Core.Abstractions;
using System;
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

            foreach(string key in data.Keys)
            {
                PropertyInfo property = type.GetProperty(key);
                if(property != null)
                {
                    Type propertyType = property.PropertyType;
                    if(data[key] == null)
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
                        var objectType = data[key].GetType();

                        if (objectType.Name == typeof(Dictionary<,>).Name)
                        {
                            ExtractDictionary(data, propertyType, key, property);
                            continue;
                        }

                        if (objectType.Name == typeof(List<>).Name)
                        {
                            ExtractList(data, propertyType, key, property);
                        }
                    }
                }
            }
        }
        
        private void ExtractList(IDictionary<string, object> data, Type propertyType, string key, PropertyInfo property)
        {
            var genericType = propertyType.GenericTypeArguments.First();
            var listType = typeof(List<>);
            var concreteType = listType.MakeGenericType(genericType);

            var list = Activator.CreateInstance(concreteType);

            var enumerable = data[key] as List<object>;
            if (enumerable != null)
                foreach (var item in enumerable)
                {
                    concreteType.GetMethod("Add")
                        .Invoke(list,
                            new[] {Convert.ChangeType(item, propertyType.GenericTypeArguments.First())});
                }

            property.SetValue(this, list);
        }

        private void ExtractDictionary(IDictionary<string, object> data, Type propertyType, string key, PropertyInfo property)
        {
            var genericType = propertyType.GetGenericArguments();
            var dicType = typeof(Dictionary<,>);
            var correctType = dicType.MakeGenericType(genericType);

            var dic = Activator.CreateInstance(correctType);

            var enumerable = data[key] as IDictionary<string, object>;
            if (enumerable != null)
            {
                foreach (var item in enumerable)
                {
                    correctType.GetMethod("Add").Invoke(dic, new[] {item.Key, item.Value});
                }
            }

            property.SetValue(this, dic);
        }
    }
}
