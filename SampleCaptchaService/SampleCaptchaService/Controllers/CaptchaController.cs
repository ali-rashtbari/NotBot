using Microsoft.AspNetCore.Mvc;
using NotBot.Services.Contracts;
using NotBot.Services.DTOs;

namespace SampleCaptchaService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController(INotBotService notBotService) : ControllerBase
    {

        [HttpGet("Build")]
        public IActionResult Build()
        {
            var clientFingerPrint = $"{HttpContext.Connection.RemoteIpAddress}:{HttpContext.Request.Headers.UserAgent}";
            var captcha = notBotService.BuildCaptcha(clientFingerPrint);
            Response.Headers["token"] = captcha.Token;

            return File(captcha.ImageArray, "image/jpeg");
        }

        [HttpGet("Verify")]
        public IActionResult Verify(string code, string token)
        {
            var clientFingerPrint = $"{HttpContext.Connection.RemoteIpAddress}:{HttpContext.Request.Headers.UserAgent}";
            var captcha = notBotService.VerifyCaptcha(new VerifyCaptchaDto(code, token, clientFingerPrint));

            return Ok(captcha);
        }
    }
}
