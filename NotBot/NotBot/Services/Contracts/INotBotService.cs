using NotBot.Services.DTOs;

namespace NotBot.Services.Contracts;

public interface INotBotService
{
    BuildCaptchaResultDto BuildCaptcha();
    bool VerifyCaptcha(VerifyCaptchaDto verifyRequest);
}
