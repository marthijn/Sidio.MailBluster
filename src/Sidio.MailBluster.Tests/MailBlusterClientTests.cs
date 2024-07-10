using System.Reflection;
using Flurl.Http.Configuration;
using Flurl.Http.Testing;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Sidio.MailBluster.Tests;

public sealed partial class MailBlusterClientTests : IDisposable
{
    private readonly HttpTest _httpTest = new ();

    private readonly IOptions<MailBlusterOptions> _options =
        Options.Create(new MailBlusterOptions { ApiKey = "key1", Url = "https://localhost/api/" });

    private readonly Fixture _fixture = new ();

    public void Dispose()
    {
        _httpTest.Dispose();
    }

    private MailBlusterClient CreateClient() => new(new FlurlClientCache(), _options, NullLogger<MailBlusterClient>.Instance);

    private static string ReadJsonData(string filename, string section)
    {
        var assembly = typeof(MailBlusterClientTests).GetTypeInfo().Assembly;
        using var resource =
            assembly.GetManifestResourceStream($"Sidio.MailBluster.Tests.Json.{section}.{filename}");
        using var reader = new StreamReader(resource!);
        var result = reader.ReadToEnd();
        return result;
    }
}