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
                        Type objectType = data[key].GetType();
                        if(objectType.Name == typeof(List<>).Name)
                        {
                            var genericType = propertyType.GenericTypeArguments.First();
                            Type listType = typeof(List<>);
                            Type concreteType = listType.MakeGenericType(genericType);

                            var list = Activator.CreateInstance(concreteType);                      

                            foreach(var item in (data[key] as List<object>))
                            {
                                concreteType.GetMethod("Add").Invoke(list, new[] { Convert.ChangeType(item, propertyType.GenericTypeArguments.First())});
                            }

                            property.SetValue(this, list);
                        }
                    }
                }
            }
        }
    }
}
