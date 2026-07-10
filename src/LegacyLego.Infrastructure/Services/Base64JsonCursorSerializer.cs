using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Domain.Shared;
using System.Text;
using System.Text.Json;

namespace LegacyLego.Infrastructure.Services;

public class Base64JsonCursorSerializer : ICursorSerializer
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        IncludeFields = true
    };

    public string Serialize<T>(T cursorData) where T : struct
    {
        string json = JsonSerializer.Serialize(cursorData, SerializerOptions);

        byte[] bytes = Encoding.UTF8.GetBytes(json);
        return Convert.ToBase64String(bytes);
    }

    public Result<T> Deserialize<T>(string cursor) where T : struct
    {
        try
        {
            if (string.IsNullOrWhiteSpace(cursor))
                return Result<T>.Failure(new Error("Cursor.Empty", "Курсор не может быть пустым"));

            byte[] bytes = Convert.FromBase64String(cursor);
            string json = Encoding.UTF8.GetString(bytes);

            var result = JsonSerializer.Deserialize<T>(json, SerializerOptions);

            return result.Equals(default(T))
                ? Result<T>.Failure(new Error("Cursor.Invalid", "Не удалось десериализовать данные курсора"))
                : Result<T>.Success(result);
        }
        catch (Exception)
        {
            return Result<T>.Failure(new Error("Cursor.Corrupted", "Токен курсора поврежден или невалиден"));
        }
    }
}