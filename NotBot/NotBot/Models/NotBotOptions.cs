namespace NotBot.Models;

public class NotBotOptions
{
    public int CharactersCount { get; set; } = 6;
    public int CaptchaCodeExpirationSeconds { get; set; } = 120;
    public string SecretKey { get; set; } = null;
}
