namespace Retrospective.Domain.SeedWork
{
    public abstract class Entity
    {
        private int? requestedHashCode;

        public virtual int Id { get; }

        public bool IsTransient() => Id == default;

        public override bool Equals(object obj)
        {
            if (!(obj is Entity))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            var entity = (Entity)obj;

            if (entity.IsTransient() || IsTransient())
                return false;

            return entity.Id == Id;
        }

        public override int GetHashCode()
        {
            if (IsTransient())
                return base.GetHashCode();

            requestedHashCode ??= Id.GetHashCode() ^ 31;

            return requestedHashCode.Value;
        }

        public static bool operator ==(Entity left, Entity right) =>
            left?.Equals(right) ?? Equals(right, null);

        public static bool operator !=(Entity left, Entity right) =>
            !(left == right);
    }
}
