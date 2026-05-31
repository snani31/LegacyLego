namespace LegacyLego.Infrastructure.Configuration.Common;

/// <summary>
/// Централизованное хранилище строковых литералов типов данных СУБД PostgreSQL.
/// Используется во Fluent API конфигурациях для обеспечения строгой типизации и предотвращения опечаток.
/// </summary>
internal static class PostgresTypes
{
    internal const string TimeStampTz = "timestamptz";
    internal const string Uuid = "uuid";
    internal const string SmallInt = "smallint";
    internal const string Text = "text";

    /// <summary>
    /// Генерирует строку точного численного типа данных <c>numeric(precision, scale)</c>.
    /// </summary>
    /// <param name="precision">Общее количество десятичных цифр в числе (как до, так и после запятой).</param>
    /// <param name="scale">Количество цифр в дробной части (после запятой).</param>
    /// <returns>Строковое представление numeric(x,n) типа данных для передачи в <c>HasColumnType()</c>.</returns>
    internal static string Numeric(int precision, int scale) => $"numeric({precision},{scale})";
}