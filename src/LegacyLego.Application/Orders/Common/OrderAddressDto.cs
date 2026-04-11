namespace LegacyLego.Application.Orders.Common;

public sealed record OrderAddressDto(
    string Country,
    string City,
    string Street,
    string PostalCode);