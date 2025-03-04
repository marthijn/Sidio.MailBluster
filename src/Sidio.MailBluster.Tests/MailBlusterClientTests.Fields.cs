using System.Net;
using RestSharp;
using Sidio.MailBluster.Requests.Fields;
using Sidio.MailBluster.Responses;

namespace Sidio.MailBluster.Tests;

public sealed partial class MailBlusterClientTests
{
    [Fact]
    public async Task GetFieldsAsync_WhenRequestIsValid_ShouldReturnFields()
    {
        // arrange
        var responseData = ReadJsonData("ReadResponse.json", "Fields");
        var restResponse = CreateResponse(HttpStatusCode.OK, responseData);

        var client = CreateClient(out var restClientMock);
        restClientMock.Setup(
                x => x.ExecuteAsync(
                    It.Is<RestRequest>(r => r.Method == Method.Get && r.Resource == MailBlusterApiConstants.Fields),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(restResponse);

        // act
        var response = await client.GetFieldsAsync();

        // assert
        response.Should().NotBeNull();
        response.Fields.Should().NotBeEmpty();
        response.Fields.Should().HaveCount(2);

        var field = response.Fields.First();
        field.Id.Should().Be(9788613);
        field.FieldLabel.Should().Be("Profession");
        field.FieldMergeTag.Should().Be("profession");
    }

    [Fact]
    public async Task CreateFieldAsync_WhenRequestIsValid_ShouldReturnField()
    {
        // arrange
        var responseData = ReadJsonData("CreateResponse.json", "Fields");
        var restResponse = CreateResponse(HttpStatusCode.Created, responseData);

        var client = CreateClient(out var restClientMock);
        restClientMock.Setup(
                x => x.ExecuteAsync(
                    It.Is<RestRequest>(r => r.Method == Method.Post && r.Resource == MailBlusterApiConstants.Fields),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(restResponse);

        var request = _fixture.Build<CreateFieldRequest>().Create();

        // act
        var response = await client.CreateFieldAsync(request);

        // assert
        response.Should().NotBeNull();
        response.Message.Should().Be("Field added");
        response.Field!.Id.Should().Be(9788646);
        response.Field.FieldLabel.Should().Be("Gender");
    }

    [Fact]
    public async Task CreateFieldAsync_WhenFeatureIsLocked_ShouldReturnHttpError()
    {
        // arrange
        var responseData = ReadJsonData("FeatureLocked.json", "Errors");
        var restResponse = CreateResponse(HttpStatusCode.Forbidden, responseData);

        var request = _fixture.Build<CreateFieldRequest>().Create();
        var client = CreateClient(out var restClientMock);
        restClientMock.Setup(
                x => x.ExecuteAsync(
                    It.Is<RestRequest>(r => r.Method == Method.Post && r.Resource == MailBlusterApiConstants.Fields),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(restResponse);

        // act
        var action = () => client.CreateFieldAsync(request);

        // assert
        await action.Should().ThrowExactlyAsync<MailBlusterHttpException>()
            .Where(x => x.ErrorCode == ErrorCode.FeatureLocked)
            .WithMessage("To access this feature, please upgrade your plan");
    }

    [Fact]
    public async Task UpdateFieldAsync_WhenFieldExists_ShouldReturnField()
    {
        // arrange
        const long Id = 9788646;
        var responseData = ReadJsonData("UpdateResponse.json", "Fields");
        var restResponse = CreateResponse(HttpStatusCode.OK, responseData);

        var request = _fixture.Build<UpdateFieldRequest>().Create();
        var client = CreateClient(out var restClientMock);
        restClientMock.Setup(
                x => x.ExecuteAsync(
                    It.Is<RestRequest>(r => r.Method == Method.Put && r.Resource == MailBlusterApiConstants.FieldsById),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(restResponse);

        // act
        var response = await client.UpdateFieldAsync(Id, request);

        // assert
        response.Should().NotBeNull();
        response.Message.Should().Be("Field updated");
        response.Field!.FieldLabel.Should().Be("Gender");
        response.Field.Id.Should().Be(Id);
        response.Field.FieldMergeTag.Should().Be("gender");
    }

    [Fact]
    public async Task DeleteFieldAsync_WhenRequestIsValid_ShouldReturnSuccess()
    {
        // arrange
        const long Id = 9788646;
        var responseData = ReadJsonData("DeleteResponse.json", "Fields");
        var restResponse = CreateResponse(HttpStatusCode.OK, responseData);

        var client = CreateClient(out var restClientMock);
        restClientMock.Setup(
                x => x.ExecuteAsync(
                    It.Is<RestRequest>(r => r.Method == Method.Delete && r.Resource == MailBlusterApiConstants.FieldsById),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(restResponse);

        // act
        var response = await client.DeleteFieldAsync(Id);

        // assert
        response.Should().NotBeNull();
        response.Id.Should().Be(Id);
        response.Message.Should().Be("Field deleted");
        response.Success.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteFieldAsync_WhenFieldDoesNotExist_ShouldReturnSuccessWithEmptyId()
    {
        // arrange
        var responseData = ReadJsonData("DeleteNotFoundResponse.json", "Fields");
        var restResponse = CreateResponse(HttpStatusCode.NotFound, responseData);

        var id = _fixture.Create<long>();
        var client = CreateClient(out var restClientMock);
        restClientMock.Setup(
                x => x.ExecuteAsync(
                    It.Is<RestRequest>(r => r.Method == Method.Delete && r.Resource == MailBlusterApiConstants.FieldsById),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(restResponse);

        // act
        var response = await client.DeleteFieldAsync(id);

        // assert
        response.Should().NotBeNull();
        response.Id.Should().BeNull();
        response.Message.Should().Be("Field not found");
        response.Success.Should().BeFalse();
    }
}