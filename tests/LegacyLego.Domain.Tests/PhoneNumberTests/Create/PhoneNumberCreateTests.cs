namespace LegacyLego.Domain.Tests.PhoneNumberTests.Create;

public class PhoneNumberCreateTests
{
    [Test]
    [Arguments("+7 (999) 123-45-67", "+79991234567")]
    [Arguments("+1-202-555-0143", "+12025550143")]
    [Arguments("  +49 30 123456  ", "+4930123456")]
    [Arguments("+380 (44) 123 45 67", "+380441234567")]
    public async Task Create_WithValidFormattedPhone_ShouldNormalizeAndReturnSuccess(
        string input,
        string expectedNormalized)
    {
        var result = PhoneNumber.Create(input);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value.Value).IsEqualTo(expectedNormalized);
    }

    [Test]
    public async Task Create_WithNullValue_ShouldThrowArgumentNullException()
    {
        var action = () => PhoneNumber.Create(null!);

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    [Arguments("")]
    [Arguments("   ")]
    public async Task Create_WithEmptyOrWhiteSpace_ShouldThrowArgumentException(string input)
    {
        var action = () => PhoneNumber.Create(input);

        await Assert.That(action).Throws<ArgumentException>();
    }

    [Test]
    [Arguments("89991234567")]          // Отсутствует ведущий '+'
    [Arguments("+09991234567")]         // Код страны начинается с 0 (запрещено ^\+[1-9])
    [Arguments("+123456789")]           // Всего 9 цифр после '+' (меньше лимита в 10)
    [Arguments("+1234567890123456")]    // 16 цифр после '+' (больше лимита в 15)
    [Arguments("+7 (999) ABC-45-67")]   // Буквенный номер (буквы вырезаются, остается < 10 цифр)
    [Arguments("invalid_phone_string")] // Полностью невалидная строка
    public async Task Create_WithInvalidPhoneFormat_ShouldReturnRegexInvalidFormatError(string invalidPhone)
    {
        var result = PhoneNumber.Create(invalidPhone);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code)
            .IsEqualTo(PhoneNumberErrors.GetCreateValueRegexInvalidFormatError(invalidPhone).Code);
    }

}
