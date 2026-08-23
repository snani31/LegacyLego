using System.Security.Cryptography;
using System.Text;

namespace LegacyLego.Presentation.Authentication.Helpers;

public static class PkceGenerator
{
    public static (string Verifier, string Challenge) GeneratePair()
    {
        var verifier = GenerateVerifier();
        var challenge = GenerateChallenge(verifier);
        return (verifier, challenge);
    }

    private static string GenerateVerifier()
    {
        var bytes = new byte[32];
        RandomNumberGenerator.Fill(bytes);
        return Base64UrlEncode(bytes);
    }

    private static string GenerateChallenge(string codeVerifier)
    {
        var challengeBytes = SHA256.HashData(Encoding.UTF8.GetBytes(codeVerifier));
        return Base64UrlEncode(challengeBytes);
    }

    private static string Base64UrlEncode(byte[] bytes)
    {
        return Convert.ToBase64String(bytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }
}