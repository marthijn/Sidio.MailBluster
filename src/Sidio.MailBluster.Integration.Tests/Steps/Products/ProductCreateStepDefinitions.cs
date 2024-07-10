using Sidio.MailBluster.Integration.Tests.Drivers.Products;
using Sidio.MailBluster.Integration.Tests.Hooks;

namespace Sidio.MailBluster.Integration.Tests.Steps.Products;

[Binding]
[Scope(Scenario = "Create a product")]
[Collection("ReqnrollNonParallelizableFeatures")]
public sealed class ProductCreateStepDefinitions
{
    private readonly ProductExistsDriver _productExistsDriver;
    private readonly CreateProductDriver _createProductDriver;
    private readonly ScenarioContext _scenarioContext;

    private string? _id;

    public ProductCreateStepDefinitions(
        ProductExistsDriver productExistsDriver,
        CreateProductDriver createProductDriver,
        ScenarioContext scenarioContext)
    {
        _productExistsDriver = productExistsDriver;
        _createProductDriver = createProductDriver;
        _scenarioContext = scenarioContext;
    }

    private string Id => _id ?? throw new InvalidOperationException("Id is not set");

    [BeforeScenario]
    public void BeforeScenario()
    {
        _id = Guid.NewGuid().ToString("N");
        _scenarioContext.SetProductId(_id);
    }

    [Given(@"a product does not exist")]
    public async Task GivenAProductDoesNotExist()
    {
        await _productExistsDriver.ProductShouldNotExistAsync(Id);
    }

    [When(@"the product is created")]
    public async Task WhenTheProductIsCreated()
    {
        await _createProductDriver.CreateProductAsync(Id);
    }

    [Then(@"the product should exist")]
    public async Task ThenTheProductShouldExist()
    {
        await _productExistsDriver.ProductShouldExistAsync(Id);
        await _productExistsDriver.ProductShouldExistInListAsync(Id);
    }
}