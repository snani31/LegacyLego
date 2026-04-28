using LegacyLego.Application.Abstractions.Messaging.Command;

namespace LegacyLego.Application.Orders.Commands.Refund;

public sealed record RefundOrderCommand(Guid OrderId) : ICommand<RefundOrderDetails>;