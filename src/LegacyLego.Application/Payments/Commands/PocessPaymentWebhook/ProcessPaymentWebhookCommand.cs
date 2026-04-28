using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Payments.Common;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public sealed record ProcessPaymentWebhookCommand(PaymentWebhook Webhook) : ICommand<ProcessPaymentDetails>;