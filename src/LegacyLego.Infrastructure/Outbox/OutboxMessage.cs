namespace LegacyLego.Infrastructure.Outbox;

public sealed class OutboxMessage
{
    /// <summary>
    /// Конструктор для создания нового экземпляра OutboxMessage 
    /// в целях его дальнейшей записи в хранилище
    /// </summary>
    /// <param name="id"> идентификатор сообщения</param>
    /// <param name="type">тип сообщения</param>
    /// <param name="content">содержание сообщение json</param>
    /// <param name="occurredOnUtc">дата и время появления сообщения в формате utc</param>
    public OutboxMessage(Guid id, string type, string content, DateTime occurredOnUtc)
    {
        Id = id;
        Type = type;
        Content = content;
        OccurredOnUtc = occurredOnUtc;
    }

    /// <summary>
    /// Приватный конструктор, используемый для материализации объекта OutboxMessage
    /// EF ORM системой в соответствии с конфигурациями
    /// </summary>
    private OutboxMessage() { }

    public Guid Id { get; init; }
    public string Type { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime OccurredOnUtc { get; init; }

    public DateTime? ProcessedOnUtc { get; set; }
    public string? Error { get; set; }
}