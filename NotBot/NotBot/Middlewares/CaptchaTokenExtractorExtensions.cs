using Microsoft.AspNetCore.Builder;

namespace NotBot.Middlewares;

public static class CaptchaTokenExtractorExtensions
{
    public static IApplicationBuilder UseCaptchaTokenExtractor(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CaptchaTokenExtractor>();
    }
}
