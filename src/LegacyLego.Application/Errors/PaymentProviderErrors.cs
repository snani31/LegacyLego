using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Errors;

public static class PaymentProviderErrors
{
    public const string SessionNotFoundCode = "PaymentProvider.SessionNotFoundByPaymentId";

    public static Error GetSessionNotFoundByPaymentIdError(Guid paymentId)
    {
        return new(
            Code: SessionNotFoundCode,
            Message: $"По следующему OrderPayment Id: {paymentId} не было найдено ни одной активной сессии оплаты");
    }

}