using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
namespace LegacyLego.Domain.ValueObjects;

public class Language : ValueObject
{
    private static readonly Dictionary<string, Language> Codes;

    public static readonly Language Russian = new("RU-RU");

    public static readonly Language English = new("EN-US");

    public string Code { get; }

    static Language()
    {
        Codes = new Dictionary<string, Language>()
        {
            { English.Code, English},
            { Russian.Code, Russian}
        };
    }

    private Language(string code)
    {
        Code = code.ToUpperInvariant();
    }

    public static Result<Language> FromCode(string code)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(code, nameof(code));

        var codeNormalized = code.Trim().ToUpperInvariant();

        if (!Codes.TryGetValue(codeNormalized, out var language))
            return Result<Language>.Failure(
                LanguageErrors.GetNotSupportedError(codeNormalized));

        return Result<Language>.Success(language);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Code;
    }
}