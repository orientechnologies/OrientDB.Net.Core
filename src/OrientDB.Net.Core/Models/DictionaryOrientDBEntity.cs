using System.Collections.Generic;

namespace OrientDB.Net.Core.Models
{
    public class DictionaryOrientDBEntity : OrientDBEntity
    {
        private readonly IDictionary<string, object> _fields = new Dictionary<string, object>();

        public IDictionary<string, object> Fields { get { return _fields; } }

        public T GetField<T>(string key)
        {
            return (T)_fields[key];
        }

        public void SetField<T>(string key, T obj)
        {
            if (_fields.ContainsKey(key))
                _fields[key] = obj;
            else
                _fields.Add(key, obj);
        }

        public DictionaryOrientDBEntity()
        {

        }

        public override void Hydrate(IDictionary<string, object> data)
        {
            foreach (var key in data.Keys)
            {
                if (_fields.ContainsKey(key))
                    _fields[key] = data[key];
                else
                    _fields.Add(key, data[key]);

            }
        }
    }
}
