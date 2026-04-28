using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public static class ProcessPaymentErrors
{
    public const string InvalidAmountCode = "Payment.InvalidAmount";
    public const string TotalPricesMismatchCode = "ProcessPayment.TotalPricesMismatch";
    public const string UnknownStatusCode = "ProcessPayment.UnknownStatus";
    public const string TransactionConflictCode = "ProcessPayment.TransactionConflict";

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

    public static Error GetUnknownStatusError(PaymentStatus unknownStatus)
    {
        return new(
            Code: UnknownStatusCode,
            Message: $"{unknownStatus} is unknown status");
    }

    public static Error GetTransactionConflictError(string TransactionId, string webhookTransactionId)
    {
        return new(
            Code: TransactionConflictCode,
            Message: $"For successed payment system get more then one different transactions by {TransactionId} and {webhookTransactionId} transactionId's");
    }

}