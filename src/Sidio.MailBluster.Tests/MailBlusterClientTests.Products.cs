using System.Net;
using RestSharp;
using Sidio.MailBluster.Requests.Products;

namespace Sidio.MailBluster.Tests;

public sealed partial class MailBlusterClientTests
{
    [Fact]
    public async Task GetProductsAsync_WhenRequestIsValid_ShouldReturnProducts()
    {
        // arrange
        var responseData = ReadJsonData("GetProductsResponse.json", "Products");
        var restResponse = CreateResponse(HttpStatusCode.OK, responseData);

        var client = CreateClient(out var restClientMock);
        restClientMock.Setup(
                x => x.ExecuteAsync(
                    It.Is<RestRequest>(r => r.Method == Method.Get && r.Resource == MailBlusterApiConstants.Products),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(restResponse);

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
    }

    [Fact]
    public async Task GetProductAsync_WhenRequestIsValid_ShouldReturnProduct()
    {
        // arrange
        var responseData = ReadJsonData("GetProductResponse.json", "Products");
        var restResponse = CreateResponse(HttpStatusCode.OK, responseData);

        var id = _fixture.Create<string>();
        var client = CreateClient(out var restClientMock);
        restClientMock.Setup(
                x => x.ExecuteAsync(
                    It.Is<RestRequest>(r => r.Method == Method.Get && r.Resource == MailBlusterApiConstants.ProductsById),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(restResponse);

        // act
        var response = await client.GetProductAsync(id);

        // assert
        response.Should().NotBeNull();
        response!.Id.Should().Be("101");
        response.Name.Should().Be("Reign Html Template");
        response.CreatedAt.Should().Be("2016-07-23T08:03:18.954Z");
        response.UpdatedAt.Should().Be("2016-11-04T01:32:12.000Z");
    }

    [Fact]
    public async Task GetProductAsync_WhenProductDoesNotExist_ShouldReturnNull()
    {
        // arrange
        var responseData = ReadJsonData("NotFoundResponse.json", "Products");
        var restResponse = CreateResponse(HttpStatusCode.NotFound, responseData);

        var id = _fixture.Create<string>();
        var client = CreateClient(out var restClientMock);
        restClientMock.Setup(
                x => x.ExecuteAsync(
                    It.Is<RestRequest>(r => r.Method == Method.Get && r.Resource == MailBlusterApiConstants.ProductsById),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(restResponse);

        // act
        var response = await client.GetProductAsync(id);

        // assert
        response.Should().BeNull();
    }

    [Fact]
    public async Task CreateProductAsync_WhenRequestIsValid_ShouldReturnProduct()
    {
        // arrange
        var responseData = ReadJsonData("CreateResponse.json", "Products");
        var restResponse = CreateResponse(HttpStatusCode.OK, responseData);

        var request = _fixture.Build<CreateProductRequest>().Create();
        var client = CreateClient(out var restClientMock);
        restClientMock.Setup(
                x => x.ExecuteAsync(
                    It.Is<RestRequest>(r => r.Method == Method.Post && r.Resource == MailBlusterApiConstants.Products),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(restResponse);

        // act
        var response = await client.CreateProductAsync(request);

        // assert
        response.Should().NotBeNull();
        response.Message.Should().Be("Product added");
        response.Product.Should().NotBeNull();
        response.Product!.Id.Should().Be("101");
        response.Product.Name.Should().Be("Reign Html Template");
    }

    [Fact]
    public async Task CreateProductAsync_UnprocessableEntity_ShouldReturnError()
    {
        // arrange
        var responseData = ReadJsonData("UnprocessableEntity.json", "Errors");
        var restResponse = CreateResponse(HttpStatusCode.UnprocessableEntity, responseData);

        var request = _fixture.Build<CreateProductRequest>().Create();
        var client = CreateClient(out var restClientMock);
        restClientMock.Setup(
                x => x.ExecuteAsync(
                    It.Is<RestRequest>(r => r.Method == Method.Post && r.Resource == MailBlusterApiConstants.Products),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(restResponse);

        // act
        var action = () => client.CreateProductAsync(request);

        // assert
        await action.Should().ThrowExactlyAsync<MailBlusterUnprocessableEntityException>()
            .Where(
                x => x.UnprocessableEntityResponse["id"] == "Order ID is required" &&
                     x.UnprocessableEntityResponse["customer"] == "Customer is required");
    }

    [Fact]
    public async Task UpdateProductAsync_WhenProductExists_ShouldReturnProduct()
    {
        // arrange
        var responseData = ReadJsonData("UpdateResponse.json", "Products");
        var restResponse = CreateResponse(HttpStatusCode.OK, responseData);

        var request = _fixture.Build<UpdateProductRequest>().Create();
        var id = _fixture.Create<string>();
        var client = CreateClient(out var restClientMock);
        restClientMock.Setup(
                x => x.ExecuteAsync(
                    It.Is<RestRequest>(r => r.Method == Method.Put && r.Resource == MailBlusterApiConstants.ProductsById),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(restResponse);

        // act
        var response = await client.UpdateProductAsync(id, request);

        // assert
        response.Should().NotBeNull();
        response.Message.Should().Be("Product updated");
        response.Product!.Name.Should().Be("Reign PRO Html Template");
    }

    [Fact]
    public async Task DeleteProductAsync_WhenRequestIsValid_ShouldReturnSuccess()
    {
        // arrange
        var responseData = ReadJsonData("DeleteResponse.json", "Products");
        var restResponse = CreateResponse(HttpStatusCode.OK, responseData);

        var id = _fixture.Create<string>();
        var client = CreateClient(out var restClientMock);
        restClientMock.Setup(
                x => x.ExecuteAsync(
                    It.Is<RestRequest>(r => r.Method == Method.Delete && r.Resource == MailBlusterApiConstants.ProductsById),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(restResponse);

        // act
        var response = await client.DeleteProductAsync(id);

        // assert
        response.Should().NotBeNull();
        response.Id.Should().Be("101");
        response.Message.Should().Be("Product deleted");
        response.Success.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteProductAsync_WhenProductDoesNotExist_ShouldReturnSuccessWithEmptyId()
    {
        // arrange
        var responseData = ReadJsonData("DeleteNotFoundResponse.json", "Products");
        var restResponse = CreateResponse(HttpStatusCode.NotFound, responseData);

        var id = _fixture.Create<string>();
        var client = CreateClient(out var restClientMock);
        restClientMock.Setup(
                x => x.ExecuteAsync(
                    It.Is<RestRequest>(r => r.Method == Method.Delete && r.Resource == MailBlusterApiConstants.ProductsById),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(restResponse);

        // act
        var response = await client.DeleteProductAsync(id);

        // assert
        response.Should().NotBeNull();
        response.Id.Should().BeNullOrEmpty();
        response.Message.Should().Be("Product not found");
        response.Success.Should().BeFalse();
    }
}