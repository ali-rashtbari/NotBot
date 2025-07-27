using NotBot.Services.DTOs;

namespace NotBot.Services.Contracts;

public interface INotBotService
{
    BuildCaptchaResultDto BuildCaptcha(string clientFingerprint);
    bool VerifyCaptcha(VerifyCaptchaDto verifyRequest);
}
