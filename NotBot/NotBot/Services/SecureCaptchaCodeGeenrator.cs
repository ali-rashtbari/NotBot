using System.Security.Cryptography;

namespace NotBot.Services;

public static class SecureCaptchaCodeGeenrator
{
    public static string Generate(int length, string allowedCharacters)
    {
        if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length), "Length must be positive.");

        var result = new char[length];
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[length];

        rng.GetBytes(bytes);

        for (int i = 0; i < length; i++)
        {
            var index = bytes[i] % allowedCharacters.Length;
            result[i] = allowedCharacters[index];
        }

        return new string(result);
    }
}
