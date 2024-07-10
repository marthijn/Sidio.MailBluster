using Sidio.MailBluster.Integration.Tests.Drivers.Products;
using Sidio.MailBluster.Integration.Tests.Hooks;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Integration.Tests.Steps.Products;

[Binding]
[Scope(Scenario = "Delete a product")]
[Collection("ReqnrollNonParallelizableFeatures")]
public sealed class ProductDeleteStepDefinitions
{
    private readonly DeleteProductDriver _deleteProductDriver;
    private readonly ProductExistsDriver _productExistsDriver;
    private readonly ScenarioContext _scenarioContext;

    private Product? _product;

    public ProductDeleteStepDefinitions(
        DeleteProductDriver deleteProductDriver,
        ProductExistsDriver productExistsDriver,
        ScenarioContext scenarioContext)
    {
        _deleteProductDriver = deleteProductDriver;
        _productExistsDriver = productExistsDriver;
        _scenarioContext = scenarioContext;
    }

    private Product Product => _product ?? throw new InvalidOperationException("Product is not set");

    [Given(@"a product exists")]
    public async Task GivenAProductExists()
    {
        var product = _scenarioContext.GetProduct() ?? throw new InvalidOperationException("Product is not set in scenario context");
        _product = await _productExistsDriver.ProductShouldExistAsync(product.Id);
    }

    [When(@"the product is deleted")]
    public async Task WhenTheProductIsDeleted()
    {
        await _deleteProductDriver.DeleteProductAsync(Product.Id);
    }

    [Then(@"the product should not exist")]
    public async Task ThenTheProductShouldNotExist()
    {
        await _productExistsDriver.ProductShouldNotExistAsync(Product.Id);
    }
}