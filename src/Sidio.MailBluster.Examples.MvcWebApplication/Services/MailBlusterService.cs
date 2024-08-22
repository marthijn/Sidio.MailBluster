using System.Diagnostics.CodeAnalysis;
using Flurl.Http.Configuration;

namespace Sidio.MailBluster.Examples.MvcWebApplication.Services;

[ExcludeFromCodeCoverage]
public sealed class MailBlusterService
{
    private const string CookieName = "MailBlusterApiKey";

    private readonly IHttpContextAccessor _httpContextAccessor;

    public MailBlusterService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IMailBlusterClient CreateClient(string baseUrl = "https://api.mailbluster.com/api/")
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            throw new InvalidOperationException("HttpContext is not available.");
        }

        var apiKey = GetApiKey() ??
                     throw new InvalidOperationException("ApiKey is not available.");

        var flurlClientCache =
            _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IFlurlClientCache>();
        var logger = _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<ILogger<MailBlusterClient>>();
        return new MailBlusterClient(flurlClientCache, baseUrl, apiKey, logger);
    }

    public void SetApiKey(string apiKey)
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            throw new InvalidOperationException("HttpContext is not available.");
        }

        _httpContextAccessor.HttpContext.Response.Cookies.Append(
            CookieName,
            apiKey,
            new CookieOptions {HttpOnly = true, Secure = true});
    }

    public string? GetApiKey()
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            throw new InvalidOperationException("HttpContext is not available.");
        }

        var cookie = _httpContextAccessor.HttpContext.Request.Cookies[CookieName];
        return string.IsNullOrWhiteSpace(cookie) ? null : cookie;
    }
}