using Sidio.MailBluster.Integration.Tests.Repositories;
using Sidio.MailBluster.Models;
using Sidio.MailBluster.Requests.Products;

namespace Sidio.MailBluster.Integration.Tests.Drivers.Products;

[Binding]
public sealed class UpdateProductDriver
{
    private readonly ProductRepository _repository;

    public UpdateProductDriver(ProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product> UpdateProductAsync(string id, string updatedName)
    {
        var result = await _repository.UpdateAsync(
            id,
            new UpdateProductRequest
            {
                Name = updatedName,
            });

        result.Should().NotBeNull();
        result.Product.Should().NotBeNull();
        result.Product!.Name.Should().Be(updatedName);

        return result.Product;
    }
}