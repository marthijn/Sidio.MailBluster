using System.Net;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;
using Reqnroll.Microsoft.Extensions.DependencyInjection;
using Sidio.MailBluster.Integration.Tests.Repositories;

namespace Sidio.MailBluster.Integration.Tests;

public sealed class SetupTestDependencies
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        ReadSettingsJson();
        var mailBlusterApiKey = Environment.GetEnvironmentVariable("MAILBLUSTER_API_KEY") ??
                                throw new InvalidOperationException(
                                    "MAILBLUSTER_API_KEY is not set in Environment, see Readme.md for more information");

        var services = new ServiceCollection();
        services.AddLogging();

        // MailBluster: The API has a rate limit of 10 requests/second and 100 requests/minute.
        services.AddScoped<AsyncRetryPolicy>(
            _ =>
            {
                var retryPolicy = Policy
                    .Handle<MailBlusterHttpException>(IsTransientError)
                    .WaitAndRetryAsync(
                        3,
                        _ =>
                        {
                            var nextAttemptIn = TimeSpan.FromSeconds(60);
                            return nextAttemptIn;
                        });

                return retryPolicy;
            });

        services
            .AddScoped<LeadRepository>()
            .AddScoped<FieldRepository>()
            .AddScoped<ProductRepository>();

        services.AddMailBluster(
            options => { options.ApiKey = mailBlusterApiKey; });

        return services;
    }

    private static void ReadSettingsJson()
    {
        if (!File.Exists(Directory.GetCurrentDirectory() + "/local.settings.json"))
        {
            return;
        }

        // xunit does not support .runsettings. read json file and set environment variables.
        using var file = File.Open(Directory.GetCurrentDirectory() + "/local.settings.json", FileMode.Open);
        var document = JsonDocument.Parse(file);
        var variables = document.RootElement.EnumerateObject();
        foreach (var variable in variables)
        {
            Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
        }
    }

    private static bool IsTransientError(MailBlusterHttpException exception)
    {
        HttpStatusCode[] transientHttpStatusCodes =
        [
            HttpStatusCode.TooManyRequests,
            HttpStatusCode.RequestTimeout,
            HttpStatusCode.BadGateway,
            HttpStatusCode.ServiceUnavailable,
            HttpStatusCode.GatewayTimeout,
        ];

        return transientHttpStatusCodes.Contains(exception.HttpStatusCode);
    }
}