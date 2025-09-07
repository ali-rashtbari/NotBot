using Microsoft.AspNetCore.Builder;

namespace NotBot.Middlewares;

public static class ClientSignatureExtractorExtensions
{
    public static IApplicationBuilder UseClientSignatureExtractor(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ClientSignatureExtractor>();
    }
}