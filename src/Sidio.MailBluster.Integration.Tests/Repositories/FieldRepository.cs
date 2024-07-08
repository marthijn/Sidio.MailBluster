using Polly.Retry;
using Sidio.MailBluster.Models;
using Sidio.MailBluster.Requests.Fields;
using Sidio.MailBluster.Responses.Fields;

namespace Sidio.MailBluster.Integration.Tests.Repositories;

public sealed class FieldRepository
{
    private readonly IMailBlusterClient _client;
    private readonly AsyncRetryPolicy _retryPolicy;

    public FieldRepository(IMailBlusterClient client, AsyncRetryPolicy retryPolicy)
    {
        _client = client;
        _retryPolicy = retryPolicy;
    }
    
    public async Task<IReadOnlyList<Field>> GetListAsync()
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var result = await _client.GetFieldsAsync();
            return result.Fields;
        });
    }

    public async Task<CreateFieldResponse> CreateAsync(CreateFieldRequest request)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var result = await _client.CreateFieldAsync(request);
            return result;
        });
    }

    public async Task<UpdateFieldResponse> UpdateAsync(long id, UpdateFieldRequest request)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var result = await _client.UpdateFieldAsync(id, request);
            return result;
        });
    }

    public async Task<DeleteFieldResponse> DeleteAsync(long id)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var result = await _client.DeleteFieldAsync(id);
            return result;
        });
    }
}