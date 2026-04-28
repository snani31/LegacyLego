using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Orders.Commands.Cancel;
using LegacyLego.Application.Orders.Common;

namespace LegacyLego.Application.Orders.Commands.Pay;

public sealed record PayOrderCommand(Guid OrderId) : ICommand<PayOrderDetails>;