using Microsoft.AspNetCore.Builder;

namespace NotBot.Middlewares;

public static class Extensions
{
    public static IApplicationBuilder UseNotBot(this IApplicationBuilder app)
    {
        app.UseMiddleware<CaptchaTokenExtractor>();
        app.UseMiddleware<ClientSignatureExtractor>();
        return app;
    }
}
