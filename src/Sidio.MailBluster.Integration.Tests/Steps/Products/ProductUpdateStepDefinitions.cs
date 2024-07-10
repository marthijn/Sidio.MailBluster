using Polly.Retry;
using Sidio.MailBluster.Integration.Tests.Drivers.Products;
using Sidio.MailBluster.Integration.Tests.Hooks;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Integration.Tests.Steps.Products;

[Binding]
[Scope(Scenario = "Update a product")]
public sealed class ProductUpdateStepDefinitions
{
    private readonly ProductExistsDriver _productExistsDriver;
    private readonly UpdateProductDriver _updateProductDriver;
    private readonly ScenarioContext _scenarioContext;
    private readonly AsyncRetryPolicy _asyncRetryPolicy;

    private Product? _product;
    private string? _productName;

    public ProductUpdateStepDefinitions(
        ProductExistsDriver productExistsDriver,
        UpdateProductDriver updateProductDriver,
        ScenarioContext scenarioContext,
        AsyncRetryPolicy asyncRetryPolicy)
    {
        _productExistsDriver = productExistsDriver;
        _updateProductDriver = updateProductDriver;
        _scenarioContext = scenarioContext;
        _asyncRetryPolicy = asyncRetryPolicy;
    }

    private Product Product => _product ?? throw new InvalidOperationException("Product is not set");

    [Given(@"a product exists")]
    public async Task GivenAProductExists()
    {
        var product = _scenarioContext.GetProduct() ?? throw new InvalidOperationException("Product is not set in scenario context");
        _product = await _productExistsDriver.ProductShouldExistAsync(product.Id);
    }

    [When(@"the product is updated")]
    public async Task WhenTheProductIsUpdated()
    {
        _productName = Guid.NewGuid().ToString("N");
        await _updateProductDriver.UpdateProductAsync(Product.Id, _productName);
    }

    [Then(@"the product should be updated")]
    public async Task ThenTheProductShouldBeUpdated()
    {
        await _asyncRetryPolicy.ExecuteAsync(async () =>
        {
            var product = await _productExistsDriver.ProductShouldExistAsync(Product.Id);
            product.Name.Should().Be(_productName);
        });
    }
}