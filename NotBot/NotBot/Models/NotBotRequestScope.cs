namespace NotBot.Models;

public static class NotBotRequestScope
{
    private static readonly AsyncLocal<string> _userAgent = new();
    public static string UserAgent
    {
        get => _userAgent.Value;
        set => _userAgent.Value = value;
    }

    private static readonly AsyncLocal<string> _ip = new();
    public static string IP
    {
        get => _ip.Value;
        set => _ip.Value = value;
    }

    public static string ClientFingerprint
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_ip.Value) || string.IsNullOrWhiteSpace(_userAgent.Value))
                throw new InvalidOperationException("Client signature (IP/UserAgent) is not set.");

            return $"{_ip.Value}-{_userAgent.Value}";
        }
    }

    private static readonly AsyncLocal<string> _captchaToken = new();

    public static string CaptchaToken
    {
        get => _captchaToken.Value;
        set => _captchaToken.Value = value;
    }
}
