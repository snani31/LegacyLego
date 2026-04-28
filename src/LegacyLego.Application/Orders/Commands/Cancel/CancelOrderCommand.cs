using LegacyLego.Application.Abstractions.Messaging.Command;

namespace LegacyLego.Application.Orders.Commands.Cancel;

public sealed record CancelOrderCommand(Guid OrderId) : ICommand<CancelletionOrderDetails>;