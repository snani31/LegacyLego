using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ExceptionalErrors;

public static class ResultExceptionalErrors
{
    public const string UnexpectedValueAccessErrorCode = "Result.UnexpectedValueAccess";

    public const string InvalidInitializationErrorCode = "Result.InvalidInitialization";

    public static ExceptionalError GetUnexpectedValueAccessError()
    {
        return new(
            Code: UnexpectedValueAccessErrorCode,
            Message: $"Некорректное обращение к Result.Value в случае, когда IsSuccess = false"
        );
    }

    public static ExceptionalError GetInvalidResultInitializationError(
        bool isSuccess,
        bool isErrorContains)
    {
        var message = InvalidInitializationErrorCode;

        switch (isSuccess)
        {
            case true when isErrorContains:
                message = "Result не может быть успешным (IsSuccess) и одновременно содержать ошибку (Error), это нарушение состояния"; break;

            case false when !isErrorContains:
                message = "Result не может быть инициализирован как Failure и одновременно с тем не содержать ошибки (Error.None)"; break;
        }

        return new(
            Code: InvalidInitializationErrorCode,
            Message: message!
        );
    }
}