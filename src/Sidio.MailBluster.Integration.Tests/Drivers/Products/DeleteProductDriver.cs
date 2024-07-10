using Sidio.MailBluster.Integration.Tests.Repositories;

namespace Sidio.MailBluster.Integration.Tests.Drivers.Products;

[Binding]
public sealed class DeleteProductDriver
{
    private readonly ProductRepository _repository;

    public DeleteProductDriver(ProductRepository repository)
    {
        _repository = repository;
    }

    public async Task DeleteProductAsync(string id)
    {
        var result = await _repository.DeleteAsync(id);
        result.Should().NotBeNull();
        result.Id.Should().Be(id);
    }
}