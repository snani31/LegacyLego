using LegacyLego.Application.Abstractions.Messaging.Command;

namespace LegacyLego.Application.Orders.Commands.Expire;

public sealed record ExpireOrderCommand(Guid OrderId) : ICommand<ExpirationOrderDetails>;