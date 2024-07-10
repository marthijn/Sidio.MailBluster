using Sidio.MailBluster.Requests.Fields;
using Sidio.MailBluster.Responses;

namespace Sidio.MailBluster.Tests;

public sealed partial class MailBlusterClientTests
{
    [Fact]
    public async Task GetFieldsAsync_WhenRequestIsValid_ShouldReturnFields()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("ReadResponse.json", "Fields"));
        var client = CreateClient();

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
        _httpTest.RespondWith(ReadJsonData("CreateResponse.json", "Fields"));

        var request = _fixture.Build<CreateFieldRequest>().Create();
        var client = CreateClient();

        // act
        var response = await client.CreateFieldAsync(request);

        // assert
        response.Should().NotBeNull();
        response.Message.Should().Be("Field added");
        response.Field!.Id.Should().Be(9788646);
        response.Field.FieldLabel.Should().Be("Gender");
        _httpTest.ShouldHaveCalled($"*/fields").WithHeader("Authorization", _options.Value.ApiKey).WithContentType("application/json").WithRequestJson(request);
    }

    [Fact]
    public async Task CreateFieldAsync_WhenFeatureIsLocked_ShouldReturnHttpError()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("FeatureLocked.json", "Errors"), 403);

        var request = _fixture.Build<CreateFieldRequest>().Create();
        var client = CreateClient();

        // act
        var action = () => client.CreateFieldAsync(request);

        // assert
        await action.Should().ThrowExactlyAsync<MailBlusterHttpException>()
            .Where(x => x.ErrorCode == ErrorCode.FeatureLocked)
            .WithMessage("To access this feature, please upgrade your plan");
        _httpTest.ShouldHaveCalled($"*/fields").WithHeader("Authorization", _options.Value.ApiKey).WithContentType("application/json").WithRequestJson(request);
    }

    [Fact]
    public async Task UpdateFieldAsync_WhenFieldExists_ShouldReturnField()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("UpdateResponse.json", "Fields"));
        const long Id = 9788646;
        var request = _fixture.Build<UpdateFieldRequest>().Create();
        var client = CreateClient();

        // act
        var response = await client.UpdateFieldAsync(Id, request);

        // assert
        response.Should().NotBeNull();
        response.Message.Should().Be("Field updated");
        response.Field!.FieldLabel.Should().Be("Gender");
        response.Field.Id.Should().Be(Id);
        response.Field.FieldMergeTag.Should().Be("gender");
        _httpTest.ShouldHaveCalled($"*/fields/{Id}").WithHeader("Authorization", _options.Value.ApiKey).WithContentType("application/json").WithRequestJson(request);
    }

    [Fact]
    public async Task DeleteFieldAsync_WhenRequestIsValid_ShouldReturnSuccess()
    {
        // arrange
        const long Id = 9788646;
        _httpTest.RespondWith(ReadJsonData("DeleteResponse.json", "Fields"));
        var client = CreateClient();

        // act
        var response = await client.DeleteFieldAsync(Id);

        // assert
        response.Should().NotBeNull();
        response.Id.Should().Be(Id);
        response.Message.Should().Be("Field deleted");
        response.Success.Should().BeTrue();
        _httpTest.ShouldHaveCalled($"*/fields/{Id}").WithHeader("Authorization", _options.Value.ApiKey);
    }

    [Fact]
    public async Task DeleteFieldAsync_WhenFieldDoesNotExist_ShouldReturnSuccessWithEmptyId()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("DeleteNotFoundResponse.json", "Fields"), 404);
        var id = _fixture.Create<long>();
        var client = CreateClient();

        // act
        var response = await client.DeleteFieldAsync(id);

        // assert
        response.Should().NotBeNull();
        response.Id.Should().BeNull();
        response.Message.Should().Be("Field not found");
        response.Success.Should().BeFalse();
        _httpTest.ShouldHaveCalled($"*/fields/{id}").WithHeader("Authorization", _options.Value.ApiKey);
    }
}