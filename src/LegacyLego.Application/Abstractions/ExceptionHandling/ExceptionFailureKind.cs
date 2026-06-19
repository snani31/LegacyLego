namespace LegacyLego.Application.Abstractions.ExceptionHandling;

/// <summary>
/// Описывает конкретный тип ошибки-исключения
/// </summary>
/// <remarks>
/// Используется в случае с возникновением исключения
/// </remarks>
public enum ExceptionFailureKind : byte
{
    /// <summary>
    /// Ошибка на уровне бизнес-логики
    /// </summary>
    DomainLevelException = 0, 
    /// <summary>
    /// Ошибка на уровне интеграции с инфраструктурой
    /// </summary>
    InfrastructureLevelException,   
    /// <summary>
    /// ошибка интеграции с внешним api
    /// </summary>
    UnhandledNetworkLevelException,
    /// <summary>
    /// ошибка неизвестного типа
    /// </summary>
    Unknown
}