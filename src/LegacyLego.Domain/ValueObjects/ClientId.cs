using LegacyLego.Domain.Exceptions;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ValueObjects;

public sealed class ClientId : ValueObject, IComparable<ClientId>
{
    public Guid Value { get; }

    public ClientId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidDomainStateException(new ExceptionalError(
                Code: "ClientId.EmptyCtorGuidValue",
                Message: "guid value for ClientId can't be Guid.Empty"));
        }

        Value = value;
    }

    public static ClientId New() => new(Guid.NewGuid());

    public static Result<ClientId> From(Guid value)
    {
        if(value == Guid.Empty)
            return Result<ClientId>.Failure(new Error(
                Code: "ClientId.EmptyFromGuidValue",
                Message: "guid value for ClientId can't be Guid.Empty"));

        return Result<ClientId>.Success(new(value));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public int CompareTo(ClientId? other) => other is null ? 1 : Value.CompareTo(other.Value);

    public static bool operator <(ClientId? left, ClientId? right) => Compare(left, right) < 0;
    public static bool operator >(ClientId? left, ClientId? right) => Compare(left, right) > 0;
    public static bool operator <=(ClientId? left, ClientId? right) => Compare(left, right) <= 0;
    public static bool operator >=(ClientId? left, ClientId? right) => Compare(left, right) >= 0;

    private static int Compare(ClientId? left, ClientId? right)
    {
        if (ReferenceEquals(left, right)) return 0;
        if (left is null) return -1;

        return left.CompareTo(right);
    }

    public static implicit operator Guid(ClientId value) => value.Value;

    public override string ToString() => Value.ToString();
}
