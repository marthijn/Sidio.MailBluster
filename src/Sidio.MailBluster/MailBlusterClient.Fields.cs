using System.Net;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Sidio.MailBluster.Requests.Fields;
using Sidio.MailBluster.Responses.Fields;

namespace Sidio.MailBluster;

public sealed partial class MailBlusterClient
{
    /// <inheritdoc />
    public async Task<GetFieldsResponse> GetFieldsAsync(CancellationToken cancellationToken = default)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("Get all fields");
        }

        var response = await DefaultClient
            .AllowHttpStatus((int)HttpStatusCode.NotFound)
            .Request(MailBlusterApiConstants.Fields)
            .GetAndHandleErrorAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        return await response.GetJsonAsync<GetFieldsResponse>().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<CreateFieldResponse> CreateFieldAsync(CreateFieldRequest request, CancellationToken cancellationToken = default)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("Creating field with Label `{FieldLabel}`", request.FieldLabel);
        }

        var response = await DefaultClient
            .Request(MailBlusterApiConstants.Fields)
            .PostJsonAndHandleErrorAsync(request, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        var result = await response.GetJsonAsync<CreateFieldResponse>().ConfigureAwait(false);
        return result;
    }

    /// <inheritdoc />
    public async Task<UpdateFieldResponse> UpdateFieldAsync(long id, UpdateFieldRequest request, CancellationToken cancellationToken = default)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("Updating field with id `{FieldId}`", id);
        }

        var response = await DefaultClient
            .Request(MailBlusterApiConstants.Fields, id.ToString())
            .PutJsonAndHandleErrorAsync(request, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        var result = await response.GetJsonAsync<UpdateFieldResponse>().ConfigureAwait(false);
        return result;
    }

    /// <inheritdoc />
    public async Task<DeleteFieldResponse> DeleteFieldAsync(long id, CancellationToken cancellationToken = default)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("Delete field with id `{FieldId}`", id);
        }

        var response = await DefaultClient
            .Request(MailBlusterApiConstants.Fields, id.ToString())
            .DeleteAndHandleErrorAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        var result = await response.GetJsonAsync<DeleteFieldResponse>().ConfigureAwait(false);
        return result;
    }
}