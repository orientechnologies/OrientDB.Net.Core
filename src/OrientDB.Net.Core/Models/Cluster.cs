using System;

namespace OrientDB.Net.Core.Models
{
    public class Cluster : IEquatable<Cluster>
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public ClusterType Type { get; set; }

        [Obsolete]
        internal string Location { get; set; }
        [Obsolete]
        internal short DataSegmentID { get; set; }
        [Obsolete]
        internal string DataSegmentName { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            // if parameter cannot be cast to ORID return false.
            Cluster other = obj as Cluster;

            if (other == null)
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return (Id * 17)
                ^ Name.GetHashCode()
                ^ Type.GetHashCode();
        }

        public static bool operator ==(Cluster left, Cluster right)
        {
            if (System.Object.ReferenceEquals(left, right))
            {
                return true;
            }

            if (((object)left == null) || ((object)right == null))
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(Cluster left, Cluster right)
        {
            return !(left == right);
        }

        public bool Equals(Cluster other)
        {
            if (other == null)
                return false;

            return Id == other.Id && String.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase) && Type == other.Type;
        }
    }
}
