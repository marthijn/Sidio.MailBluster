using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using Sidio.MailBluster.Responses;

namespace Sidio.MailBluster;

/// <summary>
/// The MailBluster client.
/// </summary>
public sealed partial class MailBlusterClient : IMailBlusterClient, IDisposable
{
    private const string AuthorizationHeader = "Authorization";

    private readonly ILogger<MailBlusterClient> _logger;
    private readonly RestClient _restClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="MailBlusterClient"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <param name="logger">The logger.</param>
    public MailBlusterClient(
        IOptions<MailBlusterOptions> options,
        ILogger<MailBlusterClient> logger) : this(options.Value.Url, options.Value.ApiKey, logger)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MailBlusterClient"/> class.
    /// </summary>
    /// <param name="baseUrl">The API base url.</param>
    /// <param name="apiKey">The API key.</param>
    /// <param name="logger">The logger.</param>
    public MailBlusterClient(
        string baseUrl,
        string apiKey,
        ILogger<MailBlusterClient> logger)
    {
        _logger = logger;

        var options = new RestClientOptions
        {
            BaseUrl = new Uri(baseUrl),
            CachePolicy = new CacheControlHeaderValue
            {
                NoCache = true,
            },
        };

        _restClient = new RestClient(options);
        _restClient.AddDefaultHeader(AuthorizationHeader, apiKey);
    }

    private MailBlusterClient(HttpMessageHandler httpMessageHandler, string baseUrl, ILogger<MailBlusterClient> logger)
    {
        _restClient = new RestClient(httpMessageHandler, configureRestClient: options =>
        {
            options.BaseUrl = new Uri(baseUrl);
        });
        _logger = logger;
    }

    internal static MailBlusterClient Create(HttpMessageHandler httpMessageHandler, string baseUrl, ILogger<MailBlusterClient> logger) =>
        new(httpMessageHandler, baseUrl, logger);

    private static string CreateMd5Hash(string input)
    {
        using var md5 = System.Security.Cryptography.MD5.Create();
        var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);

        return Convert.ToHexString(hashBytes).ToLower();
    }

    private static T GetContent<T>(RestResponse response)
    {
        if (string.IsNullOrWhiteSpace(response.Content))
        {
            throw new InvalidOperationException("Response content is empty.");
        }

        return JsonSerializer.Deserialize<T>(response.Content) ??
               throw new InvalidOperationException("Response content could not be deserialized.");
    }

    private static T HandleResponse<T>(RestResponse response)
    {
        if (response.IsSuccessful)
        {
            return GetContent<T>(response);
        }

        switch (response.StatusCode)
        {
            // handle 422
            case HttpStatusCode.UnprocessableEntity:
            {
                var entities = GetContent<UnprocessableEntityResponse>(response);
                throw new MailBlusterUnprocessableEntityException(entities);
            }

            // handle 404 error responses. when an entity is not found the client should return null instead of an exception.
            case HttpStatusCode.NotFound:
            {
                if (!string.IsNullOrWhiteSpace(response.Content))
                {
                    var notFoundResponse = GetContent<MailBlusterResponse>(response);
                    if (notFoundResponse.Message != null && MailBlusterApiConstants.NoContentResponseMessages.Contains(
                            notFoundResponse.Message,
                            StringComparer.InvariantCultureIgnoreCase))
                    {
                        throw new MailBlusterNoContentException(notFoundResponse);
                    }

                    throw new MailBlusterHttpException(
                        null,
                        notFoundResponse.Message,
                        response.StatusCode,
                        response.Content);
                }

                throw new MailBlusterHttpException(
                    null,
                    response.StatusDescription ?? "An HTTP error occurred.",
                    response.StatusCode,
                    response.Content);
            }
            default:
            {
                var errorResponse = GetContent<ErrorResponse>(response);
                throw new MailBlusterHttpException(
                    errorResponse.Code,
                    errorResponse.Message,
                    response.StatusCode,
                    response.Content);
            }
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _restClient.Dispose();
    }
}