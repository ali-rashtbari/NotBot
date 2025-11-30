using Microsoft.AspNetCore.Http;
using NotBot.Models;

namespace NotBot.Middlewares;

public class ClientSignatureExtractor(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var ua = context.Request.Headers["User-Agent"].ToString();

        NotBotRequestScope.IP = ip;
        NotBotRequestScope.UserAgent = ua;

        await next(context);
    }

}
