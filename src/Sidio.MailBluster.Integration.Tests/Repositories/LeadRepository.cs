using Polly.Retry;
using Sidio.MailBluster.Models;
using Sidio.MailBluster.Requests.Leads;
using Sidio.MailBluster.Responses.Leads;

namespace Sidio.MailBluster.Integration.Tests.Repositories;

public sealed class LeadRepository
{
    private readonly IMailBlusterClient _client;
    private readonly AsyncRetryPolicy _retryPolicy;

    public LeadRepository(IMailBlusterClient client, AsyncRetryPolicy retryPolicy)
    {
        _client = client;
        _retryPolicy = retryPolicy;
    }

    public async Task<Lead?> GetAsync(string email)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var result = await _client.GetLeadAsync(email);
            return result;
        });
    }

    public async Task<CreateLeadResponse> CreateAsync(CreateLeadRequest request)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var result = await _client.CreateLeadAsync(request);
            return result;
        });
    }

    public async Task<UpdateLeadResponse> UpdateAsync(string email, UpdateLeadRequest request)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var result = await _client.UpdateLeadAsync(email, request);
            return result;
        });
    }

    public async Task<DeleteLeadResponse> DeleteAsync(string email)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var result = await _client.DeleteLeadAsync(email);
            return result;
        });
    }
}