using Flurl.Http.Configuration;
using Microsoft.Extensions.Logging;

namespace Sidio.MailBluster.IntegrationTestRunner;

class Program
{
    static async Task Main(string[] args)
    {
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Trace);
        });

        Console.WriteLine("Integration tests for Sidio.MailBluster");
        Console.WriteLine("Enter your API key:");

        var apiKey = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            var flurlClientCache = new FlurlClientCache();

            // leads
            var leadsRunner = new LeadsRunner(flurlClientCache, apiKey, loggerFactory);
            await leadsRunner.RunAsync();
        }
        else
        {
            Console.WriteLine("API key is required.");
        }
    }
}