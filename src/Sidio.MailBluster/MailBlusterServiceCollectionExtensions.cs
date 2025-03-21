﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sidio.MailBluster;

/// <summary>
/// The MailBluster service collection extensions.
/// </summary>
public static class MailBlusterServiceCollectionExtensions
{
    /// <summary>
    /// Adds MailBluster using <see cref="IConfiguration"/>.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddMailBluster(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailBlusterOptions>(configuration);

        return services.AddMailBlusterInternal();
    }

    /// <summary>
    /// Adds MailBluster using the name of the configuration section.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configurationSection">The name of the configuration section.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddMailBluster(
        this IServiceCollection services,
        [ConstantExpected] string configurationSection = MailBlusterOptions.SectionName)
    {
        services.AddOptions<MailBlusterOptions>().Configure<IConfiguration>(
            (settings, configuration) => { configuration.GetSection(configurationSection).Bind(settings); });

        return services.AddMailBlusterInternal();
    }

    /// <summary>
    /// Adds MailBluster using the options pattern.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="options">The options.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddMailBluster(this IServiceCollection services, Action<MailBlusterOptions> options)
    {
        services.Configure(options);

        return services.AddMailBlusterInternal();
    }

    private static IServiceCollection AddMailBlusterInternal(this IServiceCollection services)
    {
        services.AddSingleton<IMailBlusterClient, MailBlusterClient>();
        return services;
    }
}