using System.Net;
using System.Text.Json;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Sidio.MailBluster.Requests.Leads;
using Sidio.MailBluster.Responses.Leads;

namespace Sidio.MailBluster;

public sealed partial class MailBlusterClient
{
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
}