using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Sidio.MailBluster;

/// <summary>
/// The MailBluster client.
/// </summary>
public sealed partial class MailBlusterClient : IMailBlusterClient
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

    private bool DebugLogEnabled => _logger.IsEnabled(LogLevel.Debug);

    private bool TraceLogEnabled => _logger.IsEnabled(LogLevel.Trace);
}