using Sidio.MailBluster.Requests.Products;

namespace Sidio.MailBluster.Tests;

public sealed partial class MailBlusterClientTests
{
    [Fact]
    public async Task GetProductsAsync_WhenRequestIsValid_ShouldReturnProducts()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("GetProductsResponse.json", "Products"));
        var client = CreateClient();

        // act
        var response = await client.GetProductsAsync();

        // assert
        response.Should().NotBeNull();
        response.Products.Should().NotBeNull();
        response.Products.Should().HaveCount(2);

        response.Meta.Should().NotBeNull();
        response.Meta.Total.Should().Be(155);
        response.Meta.PrevPageNo.Should().BeNull();
        response.Meta.PageNo.Should().Be(1);
        response.Meta.NextPageNo.Should().Be(2);
        response.Meta.PerPage.Should().Be(10);
        response.Meta.TotalPage.Should().Be(16);

        _httpTest.ShouldHaveCalled($"*/products").WithHeader("Authorization", _options.Value.ApiKey);
    }

    [Fact]
    public async Task GetProductAsync_WhenRequestIsValid_ShouldReturnProduct()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("GetProductResponse.json", "Products"));
        var id = _fixture.Create<string>();
        var client = CreateClient();

        // act
        var response = await client.GetProductAsync(id);

        // assert
        response.Should().NotBeNull();
        response!.Id.Should().Be("101");
        response.Name.Should().Be("Reign Html Template");
        response.CreatedAt.Should().Be("2016-07-23T08:03:18.954Z");
        response.UpdatedAt.Should().Be("2016-11-04T01:32:12.000Z");

        _httpTest.ShouldHaveCalled($"*/products/{id}").WithHeader("Authorization", _options.Value.ApiKey);
    }

    [Fact]
    public async Task GetProductAsync_WhenProductDoesNotExist_ShouldReturnNull()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("NotFoundResponse.json", "Products"), 404);
        var id = _fixture.Create<string>();
        var client = CreateClient();

        // act
        var response = await client.GetProductAsync(id);

        // assert
        response.Should().BeNull();

        _httpTest.ShouldHaveCalled($"*/products/{id}").WithHeader("Authorization", _options.Value.ApiKey);
    }

    [Fact]
    public async Task CreateProductAsync_WhenRequestIsValid_ShouldReturnProduct()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("CreateResponse.json", "Products"));
        var request = _fixture.Build<CreateProductRequest>().Create();
        var client = CreateClient();

        // act
        var response = await client.CreateProductAsync(request);

        // assert
        response.Should().NotBeNull();
        response.Message.Should().Be("Product added");
        response.Product.Should().NotBeNull();
        response.Product!.Id.Should().Be("101");
        response.Product.Name.Should().Be("Reign Html Template");
        _httpTest.ShouldHaveCalled($"*/products").WithHeader("Authorization", _options.Value.ApiKey).WithContentType("application/json").WithRequestJson(request);
    }

    [Fact]
    public async Task CreateProductAsync_UnprocessableEntity_ShouldReturnError()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("UnprocessableEntity.json", "Errors"), 422);
        var request = _fixture.Build<CreateProductRequest>().Create();
        var client = CreateClient();

        // act
        var action = () => client.CreateProductAsync(request);

        // assert
        await action.Should().ThrowExactlyAsync<MailBlusterUnprocessableEntityException>()
            .Where(
                x => x.UnprocessableEntityResponse["id"] == "Order ID is required" &&
                     x.UnprocessableEntityResponse["customer"] == "Customer is required");
        _httpTest.ShouldHaveCalled($"*/products").WithHeader("Authorization", _options.Value.ApiKey).WithContentType("application/json").WithRequestJson(request);
    }

    [Fact]
    public async Task UpdateProductAsync_WhenProductExists_ShouldReturnProduct()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("UpdateResponse.json", "Products"));
        var request = _fixture.Build<UpdateProductRequest>().Create();
        var id = _fixture.Create<string>();
        var client = CreateClient();

        // act
        var response = await client.UpdateProductAsync(id, request);

        // assert
        response.Should().NotBeNull();
        response.Message.Should().Be("Product updated");
        response.Product!.Name.Should().Be("Reign PRO Html Template");
        _httpTest.ShouldHaveCalled($"*/products/{id}").WithHeader("Authorization", _options.Value.ApiKey).WithContentType("application/json").WithRequestJson(request);
    }

    [Fact]
    public async Task DeleteProductAsync_WhenRequestIsValid_ShouldReturnSuccess()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("DeleteResponse.json", "Products"));
        var id = _fixture.Create<string>();
        var client = CreateClient();

        // act
        var response = await client.DeleteProductAsync(id);

        // assert
        response.Should().NotBeNull();
        response.Id.Should().Be("101");
        response.Message.Should().Be("Product deleted");
        response.Success.Should().BeTrue();
        _httpTest.ShouldHaveCalled($"*/products/{id}").WithHeader("Authorization", _options.Value.ApiKey);
    }

    [Fact]
    public async Task DeleteProductAsync_WhenProductDoesNotExist_ShouldReturnSuccessWithEmptyId()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("DeleteNotFoundResponse.json", "Products"), 404);
        var id = _fixture.Create<string>();
        var client = CreateClient();

        // act
        var response = await client.DeleteProductAsync(id);

        // assert
        response.Should().NotBeNull();
        response.Id.Should().BeNullOrEmpty();
        response.Message.Should().Be("Product not found");
        response.Success.Should().BeFalse();
        _httpTest.ShouldHaveCalled($"*/products/{id}").WithHeader("Authorization", _options.Value.ApiKey);
    }
}