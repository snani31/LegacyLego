using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Errors;

public static class OrderPaymentErrors
{
    public const string StatusTransitionFailureCode = "OrderPayment.StatusTransitionFailure";
    public const string WrongTransactionIdExchangeCode = "OrderPayment.WrongTransactionIdExchange";
    public const string CreationTimeWasNotUtcCode = "OrderPayment.CreationTimeWasNotUtc";

    public const string NowTimeWasNotUtcForAttachSessionCode = "OrderPayment.NowTimeWasNotUtcForAttachSession";
    public const string WrongStatusForExternalSessionTransitionCode = "OrderPayment.WrongStatusForExternalSessionTransition";
    public const string EnsuredSessionIsNotExpiredTransitionFailureCode = "OrderPayment.EnsuredSessionIsNotExpiredTransitionFailure";

    public static Error GetStatusTransitionFailureError(
        PaymentAction action,
        PaymentStatus actualStatus,
        PaymentStatus nextStatus)
    {
        return new(
            Code: StatusTransitionFailureCode,
            Message: $"Action {action.ToString()} не позволяет перевести OrderPayment из статуса {actualStatus.ToString()} в {nextStatus.ToString()}");
    }

    public static Error GetWrongTransactionIdExchangeError(string currentId,string nextId)
    {
        return new(
            Code: WrongTransactionIdExchangeCode,
            Message: $"Недопустимая замена текущего TransactionId:{currentId} на {nextId} в MarkAsSucceeded операции");
    }


    public static Error GetCreationTimeWasNotUtcError(DateTimeKind timeKind)
    {
        return new(
            Code: CreationTimeWasNotUtcCode,
            Message: $"Тип передаваемого времени создания OrderPayment должен быть представлен Utc, но был {timeKind}");
    }

    public static Error GetNowTimeWasNotUtcForAttachSessionError(DateTimeKind timeKind)
    {
        return new(
            Code: NowTimeWasNotUtcForAttachSessionCode,
            Message: $"Тип передаваемого времени в AttachSession должен быть представлен Utc, но был {timeKind}");
    }

    public static Error GetWrongStatusForExternalSessionTransitionError(
        OrderPaymentId paymentId,
        PaymentStatus status,
        string newSession)
    {
        return new(
            Code: WrongStatusForExternalSessionTransitionCode,
            Message: $"Для оплаты {paymentId.Value} невозможно установить сессию {newSession}," +
            $" так какдля статуса {status} не подразумевается возможности установки сессии");
    }

    public static Error GetEnsuredSessionIsNotExpiredTransitionFailureError(
        string oldSessionId,
        string newSessionId,
        OrderPaymentId paymentId)
    {
        return new(
            Code: EnsuredSessionIsNotExpiredTransitionFailureCode,
            Message: $"Не получилось установить внешнюю сессию: {newSessionId} для оплаты {paymentId.Value}" +
            $", так как для  данной оплаты уже установлена сессия: {oldSessionId}, которая ещё не просрочена");
    }

}