using Microsoft.Extensions.DependencyInjection;
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
}
