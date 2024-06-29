using System.Net;
using System.Text.Json;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sidio.MailBluster.Requests.Leads;
using Sidio.MailBluster.Responses.Leads;

namespace Sidio.MailBluster;

/// <summary>
/// The MailBluster client.
/// </summary>
public sealed class MailBlusterClient : IMailBlusterClient
{
    private const string AuthorizationHeader = "Authorization";
    private const string CacheControlHeader = "Cache-Control";
    private const string NoCache = "no-cache";

    private readonly IFlurlClient _client;
    private readonly ILogger<MailBlusterClient> _logger;
    private readonly string _apiKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="MailBlusterClient"/> class.
    /// </summary>
    /// <param name="flurlClientCache">The Flurl client cache.</param>
    /// <param name="options">The options.</param>
    /// <param name="logger">The logger.</param>
    public MailBlusterClient(
        IFlurlClientCache flurlClientCache,
        IOptions<MailBlusterOptions> options,
        ILogger<MailBlusterClient> logger) : this(flurlClientCache, options.Value.Url, options.Value.ApiKey, logger)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MailBlusterClient"/> class.
    /// </summary>
    /// <param name="flurlClientCache">The Flurl client cache.</param>
    /// <param name="baseUrl">The API base url.</param>
    /// <param name="apiKey">The API key.</param>
    /// <param name="logger">The logger.</param>
    public MailBlusterClient(
        IFlurlClientCache flurlClientCache,
        string baseUrl,
        string apiKey,
        ILogger<MailBlusterClient> logger)
    {
        _client = flurlClientCache.GetOrAdd(nameof(MailBlusterClient), baseUrl);
        _logger = logger;
        _apiKey = apiKey;
    }

    /// <inheritdoc />
    public async Task<CreateLeadResponse> CreateLeadAsync(CreateLeadRequest request, CancellationToken cancellationToken = default)
    {
        if (_logger.IsEnabled(LogLevel.Trace))
        {
            _logger.LogTrace("Creating lead {Request}", JsonSerializer.Serialize(request));
        }
        else if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("Creating lead with Email `{Email}`", request.Email.ObfuscateEmailAddress());
        }

        var response = await DefaultClient
            .Request(MailBlusterApiConstants.Leads)
            .PostJsonAsync(request, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        var result = await response.GetJsonAsync<CreateLeadResponse>().ConfigureAwait(false);
        return result;
    }

    /// <inheritdoc />
    public async Task<GetLeadResponse?> GetLeadAsync(string email, CancellationToken cancellationToken = default)
    {
        if (_logger.IsEnabled(LogLevel.Trace))
        {
            _logger.LogTrace("Get lead `{Email}`", email);
        }
        else if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("Get lead with Email `{Email}`", email.ObfuscateEmailAddress());
        }

        var response = await DefaultClient
            .AllowHttpStatus((int)HttpStatusCode.NotFound)
            .Request(MailBlusterApiConstants.Leads, CreateMd5Hash(email))
            .GetAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        if (response.StatusCode < 300)
        {
            return await response.GetJsonAsync<GetLeadResponse>().ConfigureAwait(false);
        }

        if (response.StatusCode != 404)
        {
            _logger.LogDebug("Response status code {StatusCode} for get lead `{Email}`", response.StatusCode, email.ObfuscateEmailAddress());
        }

        return null;
    }

    /// <inheritdoc />
    public async Task<UpdateLeadResponse> UpdateLeadAsync(string email, UpdateLeadRequest request, CancellationToken cancellationToken = default)
    {
        if (_logger.IsEnabled(LogLevel.Trace))
        {
            _logger.LogTrace("Updating lead {Request}", JsonSerializer.Serialize(request));
        }
        else if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("Updating lead with Email `{Email}`", email.ObfuscateEmailAddress());
        }

        var response = await DefaultClient
            .Request(MailBlusterApiConstants.Leads, CreateMd5Hash(email))
            .PutJsonAsync(request, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        var result = await response.GetJsonAsync<UpdateLeadResponse>().ConfigureAwait(false);
        return result;
    }

    /// <inheritdoc />
    public async Task<DeleteLeadResponse> DeleteLeadAsync(string email, CancellationToken cancellationToken = default)
    {
        if (_logger.IsEnabled(LogLevel.Trace))
        {
            _logger.LogTrace("Delete lead `{Email}`", email);
        }
        else if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("Delete lead with Email `{Email}`", email.ObfuscateEmailAddress());
        }

        var md5 = CreateMd5Hash(email);
        var response = await DefaultClient
            .Request(MailBlusterApiConstants.Leads, md5)
            .DeleteAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        var result = await response.GetJsonAsync<DeleteLeadResponse>().ConfigureAwait(false);
        return result;
    }

    private static string CreateMd5Hash(string input)
    {
        using var md5 = System.Security.Cryptography.MD5.Create();
        var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);

        return Convert.ToHexString(hashBytes).ToLower();
    }

    private IFlurlClient DefaultClient =>
        _client
            .WithHeader(AuthorizationHeader, _apiKey)
            .WithHeader(CacheControlHeader, NoCache);
}