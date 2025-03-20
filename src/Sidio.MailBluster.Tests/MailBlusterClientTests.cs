using System.Reflection;
using Microsoft.Extensions.Logging.Abstractions;

namespace Sidio.MailBluster.Tests;

public sealed partial class MailBlusterClientTests
{
    private const string BaseUrl = "https://localhost/api";
    private const string ApplicationJson = "application/json";

    private readonly Fixture _fixture = new ();

    private static MailBlusterClient CreateClient(MockHttpMessageHandler mockHttpMessageHandler) =>
        MailBlusterClient.Create(mockHttpMessageHandler, BaseUrl, NullLogger<MailBlusterClient>.Instance);

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