using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public static class ProcessPaymentErrors
{
    public const string InvalidAmountCode = "Payment.InvalidAmount";
    public const string TotalPricesMismatchCode = "ProcessPayment.TotalPricesMismatch";
    public const string UnknownStatusCode = "ProcessPayment.UnknownStatus";
    public const string TransactionConflictCode = "ProcessPayment.TransactionConflict";
    public const string PaymentNotFoundForWebhookCode = "ProcessPayment.PaymentNotFoundForWebhook";

    public const string EmptyTransactionIdWithSucceededWebhookCode = "ProcessPayment.EmptyTransactionIdWithSucceededWebhook";
    public const string UnexpectedPaymentStatusAfterRegistrationCode = "ProcessPayment.UnexpectedPaymentStatusAfterRegistration";

    public const string CanNotCreateCurrencyFromCodeWebhookAnomalyCode = "ProcessPayment.CanNotCreateCurrencyFromCodeWebhookAnomaly";
    public const string CanNotCreatePriceFromAmountWebhookAnomalyCode = "ProcessPayment.CanNotCreatePriceFromAmountWebhookAnomaly";

    public static Error GetInvalidAmountCodeError(decimal amount)
    {
        return new(
            Code: InvalidAmountCode,
            Message: $"Amount must be greater than zero, but it was {amount}");
    }

    public static Error GetTotalPricesMismatchError(decimal webhookAmount, decimal orderTotal)
    {
        return new(
            Code: TotalPricesMismatchCode,
            Message: $"Webhook amount:{webhookAmount} must be equivalent to order's total price: {orderTotal}");
    }

    public static Error GetEmptyTransactionIdWithSucceededWebhookError()
    {
        return new(
            Code: EmptyTransactionIdWithSucceededWebhookCode,
            Message: $"TransactionId can not be null or empty for secceeded status webhook");
    }

    public static Error GetUnknownStatusCodeError(PaymentStatus unknownStatus)
    {
        return new(
            Code: UnknownStatusCode,
            Message: $"Unknown {unknownStatus} status code from Webhook");
    }

    public static Error GetUnexpectedPaymentStatusAfterRegistrationError(PaymentStatus unexpectedStatus)
    {
        return new(
            Code: UnexpectedPaymentStatusAfterRegistrationCode,
            Message: $"Unexpected OrderPayment status after succeeded peyment registration. Is was {unexpectedStatus}");
    }

    public static Error GetTransactionConflictError(string TransactionId, string webhookTransactionId)
    {
        return new(
            Code: TransactionConflictCode,
            Message: $"For successed payment system get more then one different transactions by {TransactionId} and {webhookTransactionId} transactionId's");
    }

    public static Error GetPaymentNotFoundForWebhookError(string webhookExternalSessionId, string? webhookTransactionId)
    {
        return new(
            Code: TransactionConflictCode,
            Message: $"Payment was not fount for this webhook with " +
            $"ExternalSessionId: {webhookExternalSessionId} and" +
            $"TransactionId: {webhookTransactionId ?? "null"}");
    }

    public static Error GetCanNotCreateCurrencyFromCodeWebhookAnomalyError(string currencyCode)
    {
        return new(
            Code: CanNotCreateCurrencyFromCodeWebhookAnomalyCode,
            Message: $"Can not create Currency from {currencyCode} code from webhook");
    }

    public static Error GetCanNotCreatePriceFromAmountWebhookAnomalyError(decimal amount)
    {
        return new(
            Code: CanNotCreatePriceFromAmountWebhookAnomalyCode,
            Message: $"Can not create Price from {amount} amount from webhook");
    }
}