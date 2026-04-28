using LegacyLego.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace LegacyLego.Domain.ValueObjects;

public sealed class OrderPaymentId : ValueObject
{
    public Guid Value { get; }

    public OrderPaymentId(Guid value)
    {
        Value = value;
    }

    public static OrderPaymentId New() => new(Guid.NewGuid());

    public static OrderPaymentId From(Guid value) => new(value);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
