using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ValueObjects;

public sealed class OrderId : ValueObject
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
}
