using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Orders.Commands.Expire;
using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public sealed record ProcessPaymentDetails : ICustomLogSeverity
{
    public const string AlreadyProcessedWithTransactionIdCode = "OrderPayment.AlreadyProcessedWithTransactionId";
    public const string AlreadyProcessedCode = "OrderPayment.AlreadyProcessed";

    public const string SetSuccessedCode = "OrderPayment.SuccessfullySuccessed";
    public const string SetFailedCode = "OrderPayment.SuccessfullyFailed";
    public const string SetRefundedCode = "OrderPayment.SuccessfullyRefunded";

    public string Code { get; }
    public string Message { get; }
    public Guid OrderId { get; }
    public string CurrentStatus { get; }
    public bool StateChanged { get; }

    public bool IsWarning => Code switch
    {
        AlreadyProcessedWithTransactionIdCode => true,
        AlreadyProcessedCode => true,
        SetSuccessedCode => false,
        SetFailedCode => true,
        SetRefundedCode => false,
        _ => false
    };

    private ProcessPaymentDetails(string Code,
    Guid OrderId,
    string Message,
    string CurrentStatus,
    bool StateChanged)
    {
        this.Code = Code;
        this.OrderId = OrderId;
        this.Message = Message;
        this.CurrentStatus = CurrentStatus;
        this.StateChanged = StateChanged;
    }

    internal static ProcessPaymentDetails GetAlreadyProcessedWithTransactionIdDetails(string transactionId, Guid orderId)
    {
        return new ProcessPaymentDetails(
            Code: AlreadyProcessedWithTransactionIdCode,
            OrderId: orderId,
            Message: $"Payment with transactionId: {transactionId} is already processed",
            CurrentStatus: PaymentStatus.Succeeded.ToString(),
            false);
    }

    internal static ProcessPaymentDetails GetAlreadyProcessedDetails(string transactionId, PaymentStatus status, Guid orderId)
    {
        return new ProcessPaymentDetails(
            Code: AlreadyProcessedCode,
            OrderId: orderId,
            Message: $"Payment with transactionId: {transactionId} is already processed with {status.ToString()} state earlier",
            CurrentStatus: status.ToString(),
            false);
    }

    internal static ProcessPaymentDetails GetSetSuccessedDetails(string transactionId, Guid orderId)
    {
        return new ProcessPaymentDetails(
            Code: SetSuccessedCode,
            OrderId: orderId,
            Message: $"Payment with transactionId:{transactionId} was set Successed",
            CurrentStatus: PaymentStatus.Succeeded.ToString(),
            true);
    }

    internal static ProcessPaymentDetails GetSetFailedDetails(string transactionId, Guid orderId)
    {
        return new ProcessPaymentDetails(
            Code: SetFailedCode,
            OrderId: orderId,
            Message: $"Payment with transactionId:{transactionId} was set Failed",
            CurrentStatus: PaymentStatus.Failed.ToString(),
            true);
    }

    internal static ProcessPaymentDetails GetSetRefundedDetails(string transactionId, Guid orderId)
    {
        return new ProcessPaymentDetails(
            Code: SetRefundedCode,
            OrderId: orderId,
            Message: $"Payment with transactionId:{transactionId} was set Refunded",
            CurrentStatus: PaymentStatus.Refunded.ToString(),
            true);
    }
}