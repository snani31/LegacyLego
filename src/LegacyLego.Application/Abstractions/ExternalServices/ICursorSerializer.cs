using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.ExternalServices;

public interface ICursorSerializer
{
    public string Serialize<T>(T cursorData) where T : struct;

    public Result<T> Deserialize<T>(string cursor) where T : struct;
}