﻿using Flurl.Http.Configuration;

namespace Sidio.MailBluster.MvcWebApplication.Services;

public sealed class MailBlusterService
{
    private const string SessionKey = "MailBlusterApiKey";

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

        _httpContextAccessor.HttpContext.Session.SetString(SessionKey, apiKey);
    }

    public string? GetApiKey()
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            throw new InvalidOperationException("HttpContext is not available.");
        }

        return _httpContextAccessor.HttpContext.Session.GetString(SessionKey);
    }
}