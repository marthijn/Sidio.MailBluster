using RestSharp;
using Sidio.MailBluster.Logging;
using Sidio.MailBluster.Requests.Leads;
using Sidio.MailBluster.Responses.Leads;

namespace Sidio.MailBluster;

public sealed partial class MailBlusterClient
{
    /// <inheritdoc />
    public async Task<CreateLeadResponse> CreateLeadAsync(CreateLeadRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogCreateLeadRequest(request.Email, request);

        var restRequest = new RestRequest(MailBlusterApiConstants.Leads, Method.Post);
        restRequest.AddJsonBody(request);

        var response = await _restClient.ExecuteAsync(restRequest, cancellationToken).ConfigureAwait(false);
        var result = HandleResponse<CreateLeadResponse>(response);

        _logger.LogCreateLeadResponse(request.Email, result);
        return result;
    }

    /// <inheritdoc />
    public async Task<GetLeadResponse?> GetLeadAsync(string email, CancellationToken cancellationToken = default)
    {
        _logger.LogGetLeadRequest(email);

        try
        {
            var restRequest = new RestRequest(MailBlusterApiConstants.LeadsByHash);
            restRequest.AddUrlSegment(MailBlusterApiConstants.LeadHash, CreateMd5Hash(email));
            var response = await _restClient.ExecuteAsync(restRequest, cancellationToken).ConfigureAwait(false);
            var result = HandleResponse<GetLeadResponse>(response);
            _logger.LogGetLeadResponse(email, result);
            return result;
        }
        catch (MailBlusterNoContentException)
        {
            _logger.LogLeadNotFound(email);
        }

        return null;
    }

    /// <inheritdoc />
    public async Task<UpdateLeadResponse> UpdateLeadAsync(string email, UpdateLeadRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogUpdateLeadRequest(email, request);

        var restRequest = new RestRequest(MailBlusterApiConstants.LeadsByHash, Method.Put);
        restRequest.AddUrlSegment(MailBlusterApiConstants.LeadHash, CreateMd5Hash(email));
        restRequest.AddJsonBody(request);

        var response = await _restClient.ExecuteAsync(restRequest, cancellationToken).ConfigureAwait(false);
        var result = HandleResponse<UpdateLeadResponse>(response);

        _logger.LogUpdateLeadResponse(email, result);
        return result;
    }

    /// <inheritdoc />
    public async Task<DeleteLeadResponse> DeleteLeadAsync(string email, CancellationToken cancellationToken = default)
    {
        _logger.LogDeleteLeadRequest(email);
        try
        {
            var md5 = CreateMd5Hash(email);
            var restRequest = new RestRequest(MailBlusterApiConstants.LeadsByHash, Method.Delete);
            restRequest.AddUrlSegment(MailBlusterApiConstants.LeadHash, md5);

            var response = await _restClient.ExecuteAsync(restRequest, cancellationToken).ConfigureAwait(false);
            var result = HandleResponse<DeleteLeadResponse>(response);

            _logger.LogDeleteLeadResponse(email, result);
            return result;
        }
        catch (MailBlusterNoContentException ex)
        {
            _logger.LogLeadNotFound(email);
            return new DeleteLeadResponse
            {
                Message = ex.Message
            };
        }
    }
}