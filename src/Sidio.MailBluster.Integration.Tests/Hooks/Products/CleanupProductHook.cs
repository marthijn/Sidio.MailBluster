using Sidio.MailBluster.Integration.Tests.Context;
using Sidio.MailBluster.Integration.Tests.Repositories;
using Xunit.Abstractions;

namespace Sidio.MailBluster.Integration.Tests.Hooks.Products;

[Binding]
public sealed class CleanupProductHook
{
    private readonly ProductRepository _repository;
    private readonly ProductContext _productContext;

    public CleanupProductHook(ProductRepository repository, ProductContext productContext)
    {
        _repository = repository;
        _productContext = productContext;
    }

    [AfterScenario("products")]
    public async Task CleanupAsync(ScenarioContext scenarioContext)
    {
        var testOutputHelper = scenarioContext.GetTestOutputHelper();

        var deleteResult = false;

        if (_productContext.Product != null)
        {
            deleteResult = await DeleteProduct(_productContext.Product.Id, testOutputHelper);
        }

        if (!deleteResult)
        {
            testOutputHelper.WriteLine("No product found in product context, skipping cleanup");
        }
    }

    private async Task<bool> DeleteProduct(string id, ITestOutputHelper testOutputHelper)
    {
        var result = await _repository.DeleteAsync(id);
        testOutputHelper.WriteLine($"Trying to delete product with id `{id}`: {result.Message}");
        return !string.IsNullOrWhiteSpace(result.Id);
    }
}