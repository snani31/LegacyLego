using LegacyLego.Domain.Aggregates;

namespace LegacyLego.Domain.Shared;

public abstract class Entity<TId> : IEquatable<Entity<TId>> 
    where TId : ValueObject
{

    public TId Id { get; init; }

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Equals(entity);
    }

    public bool Equals(Entity<TId>? other)
    {
        if (other is null || other.GetType() != GetType())
            return false;

        return Id.Equals(other.Id);

    }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        if (left is null ^ right is null) return false;
        return left is null || left.Equals(right);
    }

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !(left == right);

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}