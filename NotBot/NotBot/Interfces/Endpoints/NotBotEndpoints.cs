using Microsoft.AspNetCore.Mvc;
using NotBot.Services.Contracts;
using NotBot.Services.DTOs;

namespace NotBot.Interfces.Endpoints;

[ApiController]
[Route("notbot")]
public class NotBotEndpoints(INotBotService _service) : ControllerBase
{

    [HttpGet("build")]
    public IActionResult Build()
    {
        var fingerprint = HttpContext.Connection.RemoteIpAddress + ":" + Request.Headers["User-Agent"].ToString();
        var result = _service.BuildCaptcha(fingerprint);
        Response.Headers["token"] = result.Token;
        return File(result.ImageArray, "image/jpeg");
    }

    [HttpGet("verify")]
    public IActionResult Verify([FromQuery] string code, [FromQuery] string token)
    {
        var fingerprint = HttpContext.Connection.RemoteIpAddress + ":" + Request.Headers["User-Agent"].ToString();
        var result = _service.VerifyCaptcha(new VerifyCaptchaDto(code, token, fingerprint));
        return Ok(result);
    }
}