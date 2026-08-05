using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Payments.Common;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public sealed class ProcessPaymentWebhookCommandHandler(
    IPaymentRepository paymentRepository,
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<ProcessPaymentWebhookCommand, ProcessPaymentDetails>
{
    public async Task<Result<ProcessPaymentDetails>> HandleAsync(ProcessPaymentWebhookCommand command, CancellationToken ct)
    {
        var webhook = command.Webhook;

        var orderId = OrderId.From(webhook.OrderId);

        var order = await orderRepository.GetByIdAsync(orderId, ct);
        if (order is null)
            return Result<ProcessPaymentDetails>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        var payment = webhook.TransactionId == null ?
            null : await paymentRepository.GetByTransactionIdAsync(webhook.TransactionId!, ct);

        payment ??= await paymentRepository.GetByExternalSessionIdAsync(webhook.ExternalSessionId, ct);

        if (payment is null)
        {
            return Result<ProcessPaymentDetails>.Failure(
                ProcessPaymentErrors.GetPaymentNotFoundForWebhookError(webhook.ExternalSessionId, webhook.ExternalSessionId));
        }

        var result = webhook.Status switch
        {
            PaymentStatus.Refunded => HandleRefunded(payment, webhook.TransactionId),

            PaymentStatus.Failed => HandleFailed(payment),

            PaymentStatus.Succeeded => HandleSucceeded(payment, webhook, order),

            _ => Result<ProcessPaymentDetails>.Failure(ProcessPaymentErrors.GetUnknownStatusCodeError(webhook.Status))
        };

        await unitOfWork.SaveChangesAsync(ct);
        return result;
    }
    
    private static Result<ProcessPaymentDetails> HandleFailed(
        OrderPayment payment)
    {
        if (payment.Status is PaymentStatus.Failed)
        {
            return Result<ProcessPaymentDetails>.Success(
                ProcessPaymentDetails.GetAlreadyProcessedDetails(payment.TransactionId!, payment.Status, payment.OrderId.Value));
        }

        var paymentResult = payment.MarkAsFailed();
        if (paymentResult.IsFailure)
            return Result<ProcessPaymentDetails>.Failure(paymentResult.Error);

        return Result<ProcessPaymentDetails>.Success(
            ProcessPaymentDetails.GetSetFailedDetails(payment.TransactionId!, payment.OrderId.Value));
    }

    private static Result<ProcessPaymentDetails> HandleRefunded(
        OrderPayment payment, string transactionId)
    {
        if (payment.Status is PaymentStatus.Refunded)
        {
            return Result<ProcessPaymentDetails>.Success(
                ProcessPaymentDetails.GetAlreadyProcessedDetails(payment.TransactionId!, payment.Status, payment.OrderId.Value));
        }

        var paymentResult = payment.MarkAsRefunded(transactionId);
        if (paymentResult.IsFailure)
            return Result<ProcessPaymentDetails>.Failure(paymentResult.Error);

        return Result<ProcessPaymentDetails>.Success(
            ProcessPaymentDetails.GetSetRefundedDetails(payment.TransactionId!, payment.OrderId.Value));
    }

    private static Result<ProcessPaymentDetails> HandleSucceeded(
        OrderPayment payment, 
        PaymentWebhook webhook, 
        Order order)
    {
        if (string.IsNullOrWhiteSpace(webhook.TransactionId))
            Result<ProcessPaymentDetails>.Failure(ProcessPaymentErrors.GetEmptyTransactionIdWithSucceededWebhookError());

        if (payment.Status is PaymentStatus.Succeeded && payment.TransactionId != webhook.TransactionId)
        {
            return Result<ProcessPaymentDetails>.Failure(
                ProcessPaymentErrors.GetTransactionConflictError(payment.TransactionId!, webhook.TransactionId!));
        }
        else if (payment.Status is PaymentStatus.Succeeded)
        {
            return Result<ProcessPaymentDetails>.Success(
                ProcessPaymentDetails.GetAlreadyProcessedDetails(payment.TransactionId!, payment.Status, payment.OrderId.Value));
        }

        if (webhook.Amount <= 0)
            return Result<ProcessPaymentDetails>.Failure(ProcessPaymentErrors.GetInvalidAmountCodeError(webhook.Amount));

        var currency = Currency.FromCode(webhook.Currency);
        if (currency.IsFailure)
            return Result<ProcessPaymentDetails>.Failure(
                ProcessPaymentErrors.GetCanNotCreateCurrencyFromCodeWebhookAnomalyError(webhook.Currency));

        var webhookAmountPrice = Price.Create(webhook.Amount, currency.Value);
        if (webhookAmountPrice.IsFailure)
            return Result<ProcessPaymentDetails>.Failure(
                ProcessPaymentErrors.GetCanNotCreatePriceFromAmountWebhookAnomalyError(webhook.Amount));

        var paymentRegistration = payment.RegisterPaymentReceipt(webhook.TransactionId!, webhookAmountPrice.Value);
        if (paymentRegistration.IsFailure)
            return Result<ProcessPaymentDetails>.Failure(paymentRegistration.Error);

        var result = payment.Status switch
        {
            PaymentStatus.Succeeded => Result<ProcessPaymentDetails>.Success(
                ProcessPaymentDetails.GetSetSuccessedDetails(payment.TransactionId!, payment.OrderId.Value)),

            PaymentStatus.RefundRequested => Result<ProcessPaymentDetails>.Success(
            ProcessPaymentDetails.GetSetRefundRequestedDetails(
                payment.TransactionId!,
                payment.OrderId.Value,
                payment.ActualAmount!.Sum,
                payment.ActualAmount!.Currency.Code)),

            _ => Result<ProcessPaymentDetails>.Failure(
                ProcessPaymentErrors.GetUnexpectedPaymentStatusAfterRegistrationError(payment.Status))
        };

        return result;
    }

}