using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NotBot.Middlewares;
using NotBot.Models;
using NotBot.Services;
using NotBot.Services.Contracts;

namespace NotBot;

public static class Configuration
{
    public static IServiceCollection AddNotBot(this IServiceCollection services, Action<NotBotOptions> configure)
    {
        services.Configure<NotBotOptions>(configure);

        services.AddSingleton<INotBotService, NotBotService>();

        return services;
    }

    public static IApplicationBuilder UseNotBot(this IApplicationBuilder app)
    {
        app.UseMiddleware<CaptchaTokenExtractor>();
        app.UseMiddleware<ClientSignatureExtractor>();
        return app;
    }

}

