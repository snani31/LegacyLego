using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ValueObjects;

public sealed class OrderId : ValueObject, IComparable<OrderId>
{
    public Guid Value { get; }

    public OrderId(Guid value)
    {
        Value = value;
    }

    public static OrderId New() => new(Guid.NewGuid());

    public static OrderId From(Guid value) => new(value);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public int CompareTo(OrderId? other) => other is null ? 1 : Value.CompareTo(other.Value);

    public static bool operator <(OrderId? left, OrderId? right) => Compare(left, right) < 0;
    public static bool operator >(OrderId? left, OrderId? right) => Compare(left, right) > 0;
    public static bool operator <=(OrderId? left, OrderId? right) => Compare(left, right) <= 0;
    public static bool operator >=(OrderId? left, OrderId? right) => Compare(left, right) >= 0;

    private static int Compare(OrderId? left, OrderId? right)
    {
        if (ReferenceEquals(left, right)) return 0;
        if (left is null) return -1;

        return left.CompareTo(right);
    }
}
