using Microsoft.AspNetCore.Mvc;
using NotBot.Constants;
using NotBot.Services.Contracts;

namespace NotBot.Interfces.Endpoints;

[ApiController]
[Route("notbot")]
public class NotBotEndpoints(INotBotService _service) : ControllerBase
{

    [HttpGet("build")]
    public IActionResult Build()
    {
        var result = _service.BuildCaptcha();
        Response.Headers[NotBotFlags.CaptchaTokenHeaderFieldName] = result.Token;
        return File(result.ImageArray, "image/jpeg");
    }

}