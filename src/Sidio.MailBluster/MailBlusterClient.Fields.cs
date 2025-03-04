using RestSharp;
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
        var restRequest = new RestRequest(MailBlusterApiConstants.Fields);
        var response = await _restClient.ExecuteAsync(restRequest, cancellationToken).ConfigureAwait(false);
        var result = HandleResponse<GetFieldsResponse>(response);
        _logger.LogGetFieldsResponse(result);
        return result;
    }

    /// <inheritdoc />
    public async Task<CreateFieldResponse> CreateFieldAsync(CreateFieldRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogCreateFieldRequest(request.FieldLabel, request);

        var restRequest = new RestRequest(MailBlusterApiConstants.Fields, Method.Post);
        restRequest.AddJsonBody(request);

        var response = await _restClient.ExecuteAsync(restRequest, cancellationToken).ConfigureAwait(false);
        var result = HandleResponse<CreateFieldResponse>(response);

        _logger.LogCreateFieldResponse(request.FieldLabel, result);
        return result;
    }

    /// <inheritdoc />
    public async Task<UpdateFieldResponse> UpdateFieldAsync(long id, UpdateFieldRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogUpdateFieldRequest(id, request);

        var restRequest = new RestRequest(MailBlusterApiConstants.FieldsById, Method.Put);
        restRequest.AddUrlSegment(MailBlusterApiConstants.FieldId, id.ToString());
        restRequest.AddJsonBody(request);

        var response = await _restClient.ExecuteAsync(restRequest, cancellationToken).ConfigureAwait(false);
        var result = HandleResponse<UpdateFieldResponse>(response);

        _logger.LogUpdateFieldResponse(id, result);
        return result;
    }

    /// <inheritdoc />
    public async Task<DeleteFieldResponse> DeleteFieldAsync(long id, CancellationToken cancellationToken = default)
    {
        _logger.LogDeleteFieldRequest(id);
        try
        {
            var restRequest = new RestRequest(MailBlusterApiConstants.FieldsById, Method.Delete);
            restRequest.AddUrlSegment(MailBlusterApiConstants.FieldId, id.ToString());

            var response = await _restClient.ExecuteAsync(restRequest, cancellationToken).ConfigureAwait(false);
            var result = HandleResponse<DeleteFieldResponse>(response);

            _logger.LogDeleteFieldResponse(id, result);
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