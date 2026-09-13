namespace LegacyLego.IntegrationTests.Infrastructure.Helpers;

internal class PhoneNumberGenerator
{
    internal static string GeneratePhoneNumber(Guid userId)
    {
        // Преобразуем первые 8 байт GUID в 64-битное число и берем остаток для 10 цифр
        var bytes = userId.ToByteArray();
        var number = BitConverter.ToUInt64(bytes, 0) % 10_000_000_000UL;

        // Форматируем с ведущими нулями (ровно 10 цифр)
        var tenDigits = number.ToString("D10");

        // +1 (код страны) + 10 цифр = 11 цифр после +. 
        // 1 - входит в [1-9], 10 цифр - входят в \d{9,14}
        return $"+1{tenDigits}";
    }
}
