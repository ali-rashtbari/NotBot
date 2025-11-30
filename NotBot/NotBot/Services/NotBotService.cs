using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using NotBot.Models;
using NotBot.Services.Contracts;
using NotBot.Services.DTOs;
using SixLaborsCaptcha.Core;
using Color = SixLabors.ImageSharp.Color;

namespace NotBot.Services;

public class NotBotService(IOptions<NotBotOptions> options) : INotBotService
{
    private readonly NotBotOptions _options = options.Value;
    public BuildCaptchaResultDto BuildCaptcha()
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(NotBotRequestScope.ClientFingerprint);

        var code = SecureCaptchaCodeGeenrator.Generate(_options.CharactersCount, _options.AllowedCharacters);
        var expiry = GetExpiryTimestamp(_options.CaptchaCodeExpirationSeconds);

        var signedCode = Sign(code);
        var signedFingerprint = Sign(NotBotRequestScope.ClientFingerprint);
        var signature = Sign($"{signedCode}:{expiry}:{signedFingerprint}");

        var token = $"{signedCode}:{expiry}:{signedFingerprint}:{signature}";
        var imageBytes = GenerateCaptchaImage(code);

        return new BuildCaptchaResultDto(imageBytes, token);
    }

    public bool VerifyCaptcha(VerifyCaptchaDto verifyRequest)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(verifyRequest.Code);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(verifyRequest.Token);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(NotBotRequestScope.ClientFingerprint);

        var tokenParts = verifyRequest.Token.Split(':');
        if (tokenParts.Length != 4)
            return false;

        var (signedCode, expiryStr, signedFingerprint, signature) = (tokenParts[0], tokenParts[1], tokenParts[2], tokenParts[3]);

        if (signedFingerprint != Sign(NotBotRequestScope.ClientFingerprint))
            return false;

        var expectedSignature = Sign($"{signedCode}:{expiryStr}:{signedFingerprint}");

        if (!SlowEquals(expectedSignature, signature))
            return false;

        if (!long.TryParse(expiryStr, out var expiry))
            return false;

        if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > expiry)
            return false;

        return signedCode.Equals(Sign(verifyRequest.Code), StringComparison.OrdinalIgnoreCase);
    }


    #region Private Helpers

    private static byte[] GenerateCaptchaImage(string code)
    {
        var options = new SixLaborsCaptchaOptions
        {
            DrawLines = 7,
            TextColor = [Color.Blue, Color.Black],
        };

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            options.FontFamilies = ["Liberation Sans"];
        }

        var generator = new SixLaborsCaptchaModule(options);
        return generator.Generate(code);
    }

    private string Sign(string data)
    {
        var keyBytes = Encoding.UTF8.GetBytes(_options.SecretKey);
        var dataBytes = Encoding.UTF8.GetBytes(data);

        using var hmac = new HMACSHA256(keyBytes);
        var hash = hmac.ComputeHash(dataBytes);
        return Convert.ToHexString(hash);
    }

    private static long GetExpiryTimestamp(int secondsToAdd)
    {
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds() + secondsToAdd;
    }

    private static bool SlowEquals(string a, string b)
    {
        if (a.Length != b.Length)
            return false;

        var result = 0;
        for (int i = 0; i < a.Length; i++)
            result |= a[i] ^ b[i];

        return result == 0;
    }

    #endregion

}
