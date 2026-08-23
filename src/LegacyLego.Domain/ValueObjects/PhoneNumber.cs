using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using System.Text.RegularExpressions;

namespace LegacyLego.Domain.ValueObjects;

public sealed partial class PhoneNumber : ValueObject
{
    [GeneratedRegex(@"^\+[1-9]\d{9,14}$")]
    private static partial Regex PhoneRegex();

    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static Result<PhoneNumber> Create(string value)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        var normalizedNumber = NormalizeNumber(value);

        if (!PhoneRegex().IsMatch(normalizedNumber))
            return Result<PhoneNumber>.Failure(
                PhoneNumberErrors.GetCreateValueRegexInvalidFormatError(normalizedNumber));

        return Result<PhoneNumber>.Success(new PhoneNumber(normalizedNumber));
    }

    /// <summary>
    /// Нормализует строку: удаляет форматирование (пробелы, скобки, тире) 
    /// и обеспечивает наличие ведущего знака '+' для соответствия E.164.
    /// </summary>
    private static string NormalizeNumber(string phone) 
        => Regex.Replace(phone, @"[^\d+]", "");

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;

    public override string ToString() => Value.ToString();
}