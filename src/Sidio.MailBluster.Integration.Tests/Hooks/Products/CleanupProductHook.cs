using Sidio.MailBluster.Integration.Tests.Repositories;
using Xunit.Abstractions;

namespace Sidio.MailBluster.Integration.Tests.Hooks.Products;

[Binding]
public sealed class CleanupProductHook
{
    private readonly ProductRepository _repository;

    public CleanupProductHook(ProductRepository repository)
    {
        _repository = repository;
    }

    [AfterScenario("cleanupProduct")]
    public async Task CleanupAsync(ScenarioContext scenarioContext)
    {
        var testOutputHelper = scenarioContext.GetTestOutputHelper();

        var deleteResult = false;

        var id = scenarioContext.GetProductId();
        if (!string.IsNullOrWhiteSpace(id))
        {
            deleteResult = await DeleteProduct(id, testOutputHelper);
        }

        var product = scenarioContext.GetProduct();
        if (product != null)
        {
            deleteResult = await DeleteProduct(product.Id, testOutputHelper);
        }

        if (!deleteResult)
        {
            testOutputHelper.WriteLine("No id or product found in scenario context, skipping cleanup");
        }
    }

    private async Task<bool> DeleteProduct(string id, ITestOutputHelper testOutputHelper)
    {
        var result = await _repository.DeleteAsync(id);
        testOutputHelper.WriteLine($"Trying to delete product with id `{id}`: {result.Message}");
        return !string.IsNullOrWhiteSpace(result.Id);
    }
}