using Sidio.MailBluster.Integration.Tests.Repositories;
using Sidio.MailBluster.Models;
using Sidio.MailBluster.Requests.Products;

namespace Sidio.MailBluster.Integration.Tests.Drivers.Products;

[Binding]
public sealed class CreateProductDriver
{
    private readonly ProductRepository _repository;

    public CreateProductDriver(ProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product> CreateProductAsync(string id)
    {
        var result = await _repository.CreateAsync(
            new CreateProductRequest
            {
                Id = id,
                Name = id,
            });

        result.Should().NotBeNull();
        result.Product.Should().NotBeNull();
        result.Product!.Id.Should().Be(id);

        return result.Product;
    }
}