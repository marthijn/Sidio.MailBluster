using Polly.Retry;
using Sidio.MailBluster.Models;
using Sidio.MailBluster.Requests.Products;
using Sidio.MailBluster.Responses.Products;

namespace Sidio.MailBluster.Integration.Tests.Repositories;

public sealed class ProductRepository
{
    private readonly IMailBlusterClient _client;
    private readonly AsyncRetryPolicy _retryPolicy;

    public ProductRepository(IMailBlusterClient client, AsyncRetryPolicy retryPolicy)
    {
        _client = client;
        _retryPolicy = retryPolicy;
    }
    
    public async Task<Product?> GetAsync(string id)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var result = await _client.GetProductAsync(id);
            return result;
        });
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync()
    {
        var products = new List<Product>();
        int? nextPage = 1;
        do
        {
            var result = await GetAllAsync(nextPage.Value);
            nextPage = result.Meta.NextPageNo;
            products.AddRange(result.Products);
        } while (nextPage != null);

        return products;
    }

    public async Task<CreateProductResponse> CreateAsync(CreateProductRequest request)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var result = await _client.CreateProductAsync(request);
            return result;
        });
    }

    public async Task<UpdateProductResponse> UpdateAsync(string id, UpdateProductRequest request)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var result = await _client.UpdateProductAsync(id, request);
            return result;
        });
    }

    public async Task<DeleteProductResponse> DeleteAsync(string id)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var result = await _client.DeleteProductAsync(id);
            return result;
        });
    }

    private async Task<GetProductsResponse> GetAllAsync(int page)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var result = await _client.GetProductsAsync(page);
            return result;
        });
    }
}