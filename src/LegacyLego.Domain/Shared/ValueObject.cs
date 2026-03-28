using System;
using System.Collections.Generic;
using System.Text;

namespace LegacyLego.Domain.Shared;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetAtomicValues();

    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        if (left is null ^ right is null) return false;
        return left is null || left.Equals(right);
    }

    public static bool operator !=(ValueObject? left, ValueObject? right) => !(left == right);

    public override bool Equals(object? obj)
    {
        if (obj is not ValueObject other)
            return false;

        return Equals(other);
    }

    public bool Equals(ValueObject? other)
    {
        if (other is null || other.GetType() != GetType())
            return false;

        return ValuesAreEqual(other);
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Aggregate(new HashCode(),
                (hash, value) =>
                {
                    hash.Add(value);
                    return hash;
                }).ToHashCode();
    }

    private bool ValuesAreEqual(ValueObject other)
    {
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }

}