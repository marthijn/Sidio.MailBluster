using System.Diagnostics.CodeAnalysis;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Sidio.MailBluster;

public static class MailBlusterServiceCollectionExtensions
{
    public static IServiceCollection AddMailBluster(this IServiceCollection services, IConfiguration namedConfigurationSection)
    {
        services.Configure<MailBlusterOptions>(namedConfigurationSection);

        services.TryAddSingleton<IFlurlClientCache, FlurlClientCache>();
        services.AddScoped<IMailBlusterClient, MailBlusterClient>();

        return services;
    }

    public static IServiceCollection AddMailBluster(
        this IServiceCollection services,
        [ConstantExpected] string configurationSection = MailBlusterOptions.SectionName)
    {
        services.AddOptions<MailBlusterOptions>().Configure<IConfiguration>(
            (settings, configuration) => { configuration.GetSection(configurationSection).Bind(settings); });

        services.TryAddSingleton<IFlurlClientCache, FlurlClientCache>();
        services.AddScoped<IMailBlusterClient, MailBlusterClient>();

        return services;
    }
}