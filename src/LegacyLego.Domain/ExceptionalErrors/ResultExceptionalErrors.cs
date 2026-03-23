using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ExceptionalErrors;

public static class ResultExceptionalErrors
{
    public static ExceptionalError GetUnexpectedValueAccessError()
    {
        return new(
            Code: "Result.UnexpectedValueAccess",
            Message: $"Некорректное обращение к Result.Value в случае, когда IsSuccess = false"
        );
    }

    public static ExceptionalError GetInvalidResultInitializationError(
        bool isSuccess,
        bool isErrorContains)
    {
        var message = "InvalidResultInitialization";

        switch (isSuccess)
        {
            case true when isErrorContains:
                message = "Result не может быть успешным (IsSuccess) и одновременно содержать ошибку (Error), это нарушение состояния"; break;

            case false when !isErrorContains:
                message = "Result не может быть инициализирован как Failure и одновременно с тем не содержать ошибки (Error.None)"; break;
        }

        return new(
            Code: "Result.InvalidResultInitialization",
            Message: message!
        );
    }
}