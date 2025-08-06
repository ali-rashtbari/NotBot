using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NotBot.Models;

namespace NotBot.Middlewares;

public class ClientSignatureExtractorMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var ua = context.Request.Headers["User-Agen"].ToString();

        NotBotRequestScope.IP = ip;
        NotBotRequestScope.UserAgent = ua;

        await next(context);
    }

}


public static class ClientSignatureExtractorMiddlewareExtensions
{
    public static IApplicationBuilder UseClientSignatureExtractorMiddleware(IApplicationBuilder app)
    {
        return app.UseMiddleware<ClientSignatureExtractorMiddleware>();
    }
}