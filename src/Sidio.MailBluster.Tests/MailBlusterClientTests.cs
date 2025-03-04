using System.Net;
using System.Reflection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Sidio.MailBluster.Tests;

public sealed partial class MailBlusterClientTests
{
    private readonly IOptions<MailBlusterOptions> _options =
        Options.Create(new MailBlusterOptions { ApiKey = "key1", Url = "https://localhost/api/" });

    private readonly Fixture _fixture = new ();

    private static MailBlusterClient CreateClient(out Mock<IRestClient> restClientMock)
    {
        restClientMock = new Mock<IRestClient>();
        return MailBlusterClient.Create(restClientMock.Object, NullLogger<MailBlusterClient>.Instance);
    }

    private static string ReadJsonData(string filename, string section)
    {
        var assembly = typeof(MailBlusterClientTests).GetTypeInfo().Assembly;
        using var resource =
            assembly.GetManifestResourceStream($"Sidio.MailBluster.Tests.Json.{section}.{filename}");
        using var reader = new StreamReader(resource!);
        var result = reader.ReadToEnd();
        return result;
    }

    private static RestResponse CreateResponse(HttpStatusCode httpStatusCode, string? responseData)
    {
        HttpStatusCode[] successCodes =
        [
            HttpStatusCode.OK,
            HttpStatusCode.Created
        ];

        return new RestResponse(new RestRequest())
        {
            Content = responseData,
            StatusCode = httpStatusCode,
            ResponseStatus = ResponseStatus.Completed,
            IsSuccessStatusCode = successCodes.Contains(httpStatusCode),
        };
    }
}