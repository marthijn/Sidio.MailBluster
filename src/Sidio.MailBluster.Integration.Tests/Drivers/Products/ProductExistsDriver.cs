using Polly.Retry;
using Sidio.MailBluster.Integration.Tests.Repositories;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Integration.Tests.Drivers.Products;

[Binding]
public sealed class ProductExistsDriver
{
    private readonly ProductRepository _repository;
    private readonly AsyncRetryPolicy _retryPolicy;

    public ProductExistsDriver(ProductRepository repository, AsyncRetryPolicy retryPolicy)
    {
        _repository = repository;
        _retryPolicy = retryPolicy;
    }
    
    public async Task ProductShouldNotExistAsync(string id)
    {
        var result = await _repository.GetAsync(id);
        result.Should().BeNull();
    }

    public async Task<Product> ProductShouldExistAsync(string id, string? expectedProductName = null)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var result = await _repository.GetAsync(id);
            result.Should().NotBeNull();
            result!.Id.Should().Be(id);

            if (expectedProductName is not null)
            {
                result.Name.Should().Be(expectedProductName);
            }

            return result;
        });
    }

    public async Task<Product> ProductShouldExistInListAsync(string id)
    {
        var result = await _repository.GetAllAsync();
        result.Should().NotBeNull();

        var product = result.FirstOrDefault(x => x.Id == id);
        product.Should().NotBeNull();

        return product!;
    }
}