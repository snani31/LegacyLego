using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Orders.Common;

namespace LegacyLego.Application.Orders.Commands.Create;

public sealed record CreateOrderCommand(
    ExternalUserProfile UserProfile,
    string CurrencyCode,
    OrderAddressDto OrderAddress,
    List<OrderItemDto> Items) : ICommand<Guid>;