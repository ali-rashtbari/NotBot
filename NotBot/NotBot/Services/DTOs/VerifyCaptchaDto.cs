namespace NotBot.Services.DTOs;

public record VerifyCaptchaDto(string Code, string Token, string ClientFingerprint);
