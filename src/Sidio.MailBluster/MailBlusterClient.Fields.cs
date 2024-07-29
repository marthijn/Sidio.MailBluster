using Sidio.MailBluster.Logging;
using Sidio.MailBluster.Requests.Fields;
using Sidio.MailBluster.Responses.Fields;

namespace Sidio.MailBluster;

public sealed partial class MailBlusterClient
{
    /// <inheritdoc />
    public async Task<GetFieldsResponse> GetFieldsAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogGetFieldsRequest();
        var response = await DefaultClient
            .Request(MailBlusterApiConstants.Fields)
            .GetAndHandleErrorAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        var result = await response.GetJsonAsync<GetFieldsResponse>().ConfigureAwait(false);
        _logger.LogGetFieldsResponse(result);
        return result;
    }

    /// <inheritdoc />
    public async Task<CreateFieldResponse> CreateFieldAsync(CreateFieldRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogCreateFieldRequest(request.FieldLabel, request);
        var response = await DefaultClient
            .Request(MailBlusterApiConstants.Fields)
            .PostJsonAndHandleErrorAsync(request, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        var result = await response.GetJsonAsync<CreateFieldResponse>().ConfigureAwait(false);
        _logger.LogCreateFieldResponse(request.FieldLabel, result);
        return result;
    }

    /// <inheritdoc />
    public async Task<UpdateFieldResponse> UpdateFieldAsync(long id, UpdateFieldRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogUpdateFieldRequest(id, request);
        var response = await DefaultClient
            .Request(MailBlusterApiConstants.Fields, id.ToString())
            .PutJsonAndHandleErrorAsync(request, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        var result = await response.GetJsonAsync<UpdateFieldResponse>().ConfigureAwait(false);
        _logger.LogUpdateFieldResponse(id, result);
        return result;
    }

    /// <inheritdoc />
    public async Task<DeleteFieldResponse> DeleteFieldAsync(long id, CancellationToken cancellationToken = default)
    {
        _logger.LogDeleteFieldRequest(id);
        try
        {
            var response = await DefaultClient
                .Request(MailBlusterApiConstants.Fields, id.ToString())
                .DeleteAndHandleErrorAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            var result = await response.GetJsonAsync<DeleteFieldResponse>().ConfigureAwait(false);
            _logger.LogDeleteFieldResponse(id, new DeleteFieldResponse());
            return result;
        }
        catch (MailBlusterNoContentException ex)
        {
            _logger.LogFieldNotFound(id);
            return new DeleteFieldResponse
            {
                Message = ex.Message
            };
        }
    }
}