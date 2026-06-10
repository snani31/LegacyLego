using LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;
using LegacyLego.Application.Payments.Common;
using LegacyLego.Domain.Enums;
using LegacyLego.Presentation.Payments.Dto;

namespace LegacyLego.Presentation.Payments;

public static class PaymentWebhookMapper
{
    private const string REFUND_STATUS = "refund";
    private const string SUCCESS_STATUS = "success";
    private const string FAILED_STATUS = "fail";

    public static ProcessPaymentWebhookCommand MapToPaymentWebhookCommand(PaymentProviderWebhookRequest request)
    {
        var webhook = new PaymentWebhook(
            TransactionId: request.TransactionId,
            OrderId: request.OrderId,
            Amount: request.Amount,
            Currency: request.Currency,
            Status: MapRequestStatusToPaymentStatus(request.Status));

        return new ProcessPaymentWebhookCommand(webhook);
    }

    private static PaymentStatus MapRequestStatusToPaymentStatus(string requestStatus) => requestStatus switch
    {
        REFUND_STATUS => PaymentStatus.Refunded,
        SUCCESS_STATUS => PaymentStatus.Succeeded,
        FAILED_STATUS => PaymentStatus.Failed,
        _ => throw new ArgumentException(
            $"Unsupported status value: '{requestStatus}'. Allowed values: {SUCCESS_STATUS}, {FAILED_STATUS}, {REFUND_STATUS}")
    };
}