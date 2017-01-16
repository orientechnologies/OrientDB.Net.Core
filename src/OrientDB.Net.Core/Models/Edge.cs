using OrientDB.Net.Core.Attributes;
using System;

namespace OrientDB.Net.Core.Models
{
    public class Edge : DictionaryOrientDBEntity
    {
        [OrientDBProperty(Alias = "in", Serializable = false)]
        public ORID InV
        {
            get
            {
                throw new NotImplementedException();
                //return this.GetField<ORID>("in");
            }
        }

        [OrientDBProperty(Alias = "out", Serializable = false)]
        public ORID OutV
        {
            get
            {
                throw new NotImplementedException();
                //return this.GetField<ORID>("out");
            }
        }

        [OrientDBProperty(Alias = "label", Serializable = false)]
        public string Label
        {
            get
            {
                throw new NotImplementedException();
                string label = GetField<string>("@OClassName");

                if (string.IsNullOrEmpty(label))
                {
                    return this.GetType().Name;
                }
                else
                {
                    return label;
                }
            }
        }
    }
}
