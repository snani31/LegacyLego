using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Orders.Common;

namespace LegacyLego.Application.Orders.Commands.Create;

public sealed record CreateOrderCommand(
    Guid ClientId,
    string CurrencyCode,
    OrderAddressDto OrderAddress,
    List<OrderItemDto> Items) : ICommand<Guid>;