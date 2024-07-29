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
        var response = await DefaultClient
            .Request(MailBlusterApiConstants.Leads)
            .PostJsonAndHandleErrorAsync(request, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        var result = await response.GetJsonAsync<CreateLeadResponse>().ConfigureAwait(false);
        _logger.LogCreateLeadResponse(request.Email, result);
        return result;
    }

    /// <inheritdoc />
    public async Task<GetLeadResponse?> GetLeadAsync(string email, CancellationToken cancellationToken = default)
    {
        _logger.LogGetLeadRequest(email);

        try
        {
            var response = await DefaultClient
                .Request(MailBlusterApiConstants.Leads, CreateMd5Hash(email))
                .GetAndHandleErrorAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            var result = await response.GetJsonAsync<GetLeadResponse>().ConfigureAwait(false);
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
        var response = await DefaultClient
            .Request(MailBlusterApiConstants.Leads, CreateMd5Hash(email))
            .PutJsonAndHandleErrorAsync(request, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        var result = await response.GetJsonAsync<UpdateLeadResponse>().ConfigureAwait(false);
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
            var response = await DefaultClient
                .Request(MailBlusterApiConstants.Leads, md5)
                .DeleteAndHandleErrorAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            var result = await response.GetJsonAsync<DeleteLeadResponse>().ConfigureAwait(false);
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