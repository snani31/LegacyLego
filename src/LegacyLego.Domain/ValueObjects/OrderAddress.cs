using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ValueObjects;

public class OrderAddress : ValueObject
{
    public string Country { get; }

    public string City { get; }

    public string Street { get; }

    public string PostalCode { get; }

    private OrderAddress(
        string country,
        string city,
        string street,
        string postalCode)
    {
        Country = country;
        City = city;
        Street = street;
        PostalCode = postalCode;
    }

    public static Result<OrderAddress> FromCode(
        string country,
        string city,
        string street,
        string postalCode)
    {
        return Result<OrderAddress>.Success(new OrderAddress(
            country,
            city,
            street,
            postalCode
        ));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Country;
        yield return City;
        yield return Street;
        yield return PostalCode;
    }
}