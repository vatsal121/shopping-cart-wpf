using System;
using System.Collections.Generic;
using System.Linq;

namespace Utility.ValueObjects
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object other)
        {
            if (!(other is ValueObject otherValueObject))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return GetEqualityComponents().SequenceEqual(otherValueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            foreach (object obj in GetEqualityComponents())
            {
                hash.Add(obj);
            }
            return hash.ToHashCode();
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }
    }
}
