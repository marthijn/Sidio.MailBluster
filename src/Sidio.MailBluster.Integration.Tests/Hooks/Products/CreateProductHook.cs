using Sidio.MailBluster.Integration.Tests.Repositories;
using Sidio.MailBluster.Requests.Products;

namespace Sidio.MailBluster.Integration.Tests.Hooks.Products;

[Binding]
public sealed class CreateProductHook
{
    private readonly ProductRepository _repository;

    public CreateProductHook(ProductRepository repository)
    {
        _repository = repository;
    }

    [BeforeScenario("createProduct")]
    public async Task CreateLeadAsync(ScenarioContext scenarioContext)
    {
        var testOutputHelper = scenarioContext.GetTestOutputHelper();
        var id = Guid.NewGuid().ToString("N");
        var result = await _repository.CreateAsync(
            new CreateProductRequest
            {
                Id = id,
                Name = id,
            });

        result.Should().NotBeNull();
        result.Product.Should().NotBeNull();
        result.Product!.Id.Should().Be(id);

        testOutputHelper.WriteLine($"Created product with id `{result.Product.Id}`");

        scenarioContext.SetProduct(result.Product);
    }
}