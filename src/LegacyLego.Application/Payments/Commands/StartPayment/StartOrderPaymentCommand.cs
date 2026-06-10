using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

namespace LegacyLego.Application.Payments.Commands.StartPayment;

public sealed record StartOrderPaymentCommand(Guid OrderId, Guid ClientId) : ICommand<StartOrderPaymentDetails>;