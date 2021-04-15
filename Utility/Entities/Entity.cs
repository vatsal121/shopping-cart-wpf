using System;

namespace Utility.Entities
{
    public abstract class Entity
    {
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public Entity()
        { }

        public Entity(long id, DateTime dateCreated, DateTime dateModified)
        {
            Id = id;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }

        public override bool Equals(object other)
        {
            if (!(other is Entity otherEntity))
                return false;

            if (ReferenceEquals(this, otherEntity))
                return true;

            if (GetType() != otherEntity.GetType())
                return false;

            return Id == otherEntity.Id;
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} entity {Id}";
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }
    }
}
