using System;

namespace OrientDB.Net.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OrientDBProperty : Attribute
    {
        public string Alias { get; set; }
        public bool Serializable { get; set; }
        public bool Deserializable { get; set; }
        // TODO:
        //public OType Type { get; set; }

        public OrientDBProperty()
        {
            Alias = "";
            Serializable = true;
            Deserializable = true;
        }
    }
}
