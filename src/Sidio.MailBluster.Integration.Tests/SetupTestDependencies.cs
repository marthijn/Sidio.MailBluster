using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll.Microsoft.Extensions.DependencyInjection;

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
}