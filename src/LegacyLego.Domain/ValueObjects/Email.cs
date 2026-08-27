using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using System.Text.RegularExpressions;

namespace LegacyLego.Domain.ValueObjects;

public sealed partial class Email : ValueObject
{
    [GeneratedRegex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.IgnoreCase)]
    private static partial Regex EmailRegex();

    public string Value { get; }

    public string LocalPart => Value.Split('@')[0];

    public string Domain => Value.Split('@')[1];

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string value)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        var normalizedEmail = value.Trim().ToLowerInvariant();

        if (normalizedEmail.Length > 256)
            return Result<Email>.Failure(EmailErrors.GetCreateValueTooLongError(normalizedEmail.Length));

        if (!EmailRegex().IsMatch(normalizedEmail))
            return Result<Email>.Failure(EmailErrors.GetCreateValueRegexInvalidFormatError(normalizedEmail));

        return Result<Email>.Success(new Email(normalizedEmail));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static implicit operator string(Email email) => email.Value;

    public override string ToString() => Value;
}