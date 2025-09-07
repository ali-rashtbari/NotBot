using Microsoft.AspNetCore.Http;
using NotBot.Constants;
using NotBot.Models;

namespace NotBot.Middlewares;

public class CaptchaTokenExtractor(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        var isCaptchaTokenRecieved = httpContext.Request.Headers.TryGetValue(NotBotFlags.CaptchaTokenHeaderFieldName, out var captchaToken);
        if (isCaptchaTokenRecieved)
        {
            NotBotRequestScope.CaptchaToken = captchaToken;
        }

        await next(httpContext);
    }
}
