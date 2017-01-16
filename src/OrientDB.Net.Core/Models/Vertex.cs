using System.Collections.Generic;

namespace OrientDB.Net.Core.Models
{
    public class Vertex : DictionaryOrientDBEntity
    {
        public HashSet<ORID> InE
        {
            get
            {
                return this.GetField<HashSet<ORID>>("in_");
            }
        }

        public HashSet<ORID> OutE
        {
            get
            {
                return this.GetField<HashSet<ORID>>("out_");
            }
        }

        public Vertex()
        {
            this.OClassName = "V";
        }
    }
}
