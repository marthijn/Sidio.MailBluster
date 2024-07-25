using Sidio.MailBluster.Integration.Tests.Context;
using Sidio.MailBluster.Integration.Tests.Drivers.Products;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Integration.Tests.Steps;

[Binding]
[Scope(Tag = "products")]
[Collection("ReqnrollNonParallelizableFeatures")]
public sealed class ProductsStepDefinitions
{
    private readonly ProductContext _productContext;
    private readonly ProductExistsDriver _productExistsDriver;
    private readonly CreateProductDriver _createProductDriver;
    private readonly DeleteProductDriver _deleteProductDriver;
    private readonly UpdateProductDriver _updateProductDriver;

    public ProductsStepDefinitions(
        ProductContext productContext,
        ProductExistsDriver productExistsDriver,
        CreateProductDriver createProductDriver,
        DeleteProductDriver deleteProductDriver,
        UpdateProductDriver updateProductDriver)
    {
        _productContext = productContext;
        _productExistsDriver = productExistsDriver;
        _createProductDriver = createProductDriver;
        _deleteProductDriver = deleteProductDriver;
        _updateProductDriver = updateProductDriver;
    }

    private string Id => _productContext.Id ?? throw new InvalidOperationException("Id is not set");

    private Product Product => _productContext.Product ?? throw new InvalidOperationException("Product is not set");

    [Given(@"a product does not exist")]
    public async Task GivenAProductDoesNotExist()
    {
        await _productExistsDriver.ProductShouldNotExistAsync(Id);
    }

    [Given(@"a product exists")]
    public async Task GivenAProductExists()
    {
        _productContext.Product = await _createProductDriver.CreateProductAsync(Id);
    }

    [When(@"the product is created")]
    public async Task WhenTheProductIsCreated()
    {
        _productContext.Product = await _createProductDriver.CreateProductAsync(Id);
    }

    [When(@"the product is updated")]
    public async Task WhenTheProductIsUpdated()
    {
        _productContext.Name = Guid.NewGuid().ToString("N");
        await _updateProductDriver.UpdateProductAsync(Product.Id, _productContext.Name);
    }

    [When(@"the product is deleted")]
    public async Task WhenTheProductIsDeleted()
    {
        await _deleteProductDriver.DeleteProductAsync(Product.Id);
    }

    [Then(@"the product should exist")]
    public async Task ThenTheProductShouldExist()
    {
        await _productExistsDriver.ProductShouldExistAsync(Id);
        await _productExistsDriver.ProductShouldExistInListAsync(Id);
    }

    [Then(@"the product should not exist")]
    public async Task ThenTheProductShouldNotExist()
    {
        await _productExistsDriver.ProductShouldNotExistAsync(Product.Id);
        _productContext.Product = null;
    }

    [Then(@"the product should be updated")]
    public async Task ThenTheProductShouldBeUpdated()
    {
        await _productExistsDriver.ProductShouldExistAsync(
            Product.Id,
            _productContext.Name ?? throw new InvalidOperationException("Name is not set"));
    }
}