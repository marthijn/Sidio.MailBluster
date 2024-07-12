using Sidio.MailBluster.Requests.Leads;

namespace Sidio.MailBluster.Tests;

public sealed partial class MailBlusterClientTests
{
    [Fact]
    public async Task CreateLeadAsync_WhenRequestIsValid_ShouldReturnLead()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("CreateResponse.json", "Leads"));

        var fields = new Dictionary<string, string> {{_fixture.Create<string>(), _fixture.Create<string>()}};

        var request = _fixture.Build<CreateLeadRequest>().With(x => x.Fields, fields).Create();
        var client = CreateClient();

        // act
        var response = await client.CreateLeadAsync(request);

        // assert
        response.Should().NotBeNull();
        response.Message.Should().Be("Lead added");
        response.Lead!.Email.Should().Be("richard@example.com");
        response.Lead.Id.Should().Be(329395);
        _httpTest.ShouldHaveCalled($"*/leads").WithHeader("Authorization", _options.Value.ApiKey).WithContentType("application/json").WithRequestJson(request);
    }

    [Fact]
    public async Task CreateLeadAsync_UnprocessableEntity_ShouldReturnError()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("UnprocessableEntity.json", "Errors"), 422);

        var fields = new Dictionary<string, string> {{_fixture.Create<string>(), _fixture.Create<string>()}};

        var request = _fixture.Build<CreateLeadRequest>().With(x => x.Fields, fields).Create();
        var client = CreateClient();

        // act
        var action = () => client.CreateLeadAsync(request);

        // assert
        await action.Should().ThrowExactlyAsync<MailBlusterUnprocessableEntityException>()
            .Where(
                x => x.UnprocessableEntityResponse["id"] == "Order ID is required" &&
                     x.UnprocessableEntityResponse["customer"] == "Customer is required");
        _httpTest.ShouldHaveCalled($"*/leads").WithHeader("Authorization", _options.Value.ApiKey)
            .WithContentType("application/json").WithRequestJson(request);
    }

    [Fact]
    public async Task UpdateLeadAsync_WhenLeadExists_ShouldReturnLead()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("UpdateResponse.json", "Leads"));
        const string RequestEmail = "noreply@sidio.nl";
        const string RequestMd5 = "949c658fa59ccb5a816400a4b0ad36f8";
        var request = _fixture.Build<UpdateLeadRequest>()
            .With(x => x.Email, RequestEmail).Create();
        var client = CreateClient();

        // act
        var response = await client.UpdateLeadAsync(RequestEmail, request);

        // assert
        response.Should().NotBeNull();
        response.Message.Should().Be("Lead updated");
        response.Lead!.Email.Should().Be("richard@example.com");
        response.Lead.Id.Should().Be(329395);
        _httpTest.ShouldHaveCalled($"*/leads/{RequestMd5}").WithHeader("Authorization", _options.Value.ApiKey).WithContentType("application/json").WithRequestJson(request);
    }

    [Fact]
    public async Task GetLeadAsync_WhenRequestIsValid_ShouldReturnLead()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("ReadResponse.json", "Leads"));
        const string RequestEmail = "noreply@sidio.nl";
        const string RequestMd5 = "949c658fa59ccb5a816400a4b0ad36f8";
        var client = CreateClient();

        // act
        var response = await client.GetLeadAsync(RequestEmail);

        // assert
        response.Should().NotBeNull();
        response!.Email.Should().Be("richard@example.com");
        response.FirstName.Should().Be("Richard");
        response.LastName.Should().Be("Hendricks");
        response.FullName.Should().Be("Richard Hendricks");
        response.Timezone.Should().Be("America/Los_Angeles");
        response.IpAddress.Should().Be("162.213.1.246");
        response.Subscribed.Should().Be(false);
        response.OptInStatus.Should().Be("waiting");
        response.CreatedAt.Should().Be("2016-07-23T08:03:18.954Z");
        response.UpdatedAt.Should().Be("2016-07-23T08:03:18.954Z");
        response.Fields.Should().BeEquivalentTo(new Dictionary<string, string> {{"gender", "Male"}, {"address", "Silicon Valley"}});
        response.Tags.Should().BeEquivalentTo(["iPhone User", "Startup"]);
        _httpTest.ShouldHaveCalled($"*/leads/{RequestMd5}").WithHeader("Authorization", _options.Value.ApiKey);
    }

    [Fact]
    public async Task GetLeadAsync_WhenApiEndpointDoesNotExist_ShouldReturnError()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("ApiEndpointDoesNotExist.json", "Errors"), 404);
        const string RequestEmail = "noreply@sidio.nl";
        const string RequestMd5 = "949c658fa59ccb5a816400a4b0ad36f8";
        var client = CreateClient();

        // act
        var action = () => client.GetLeadAsync(RequestEmail);

        // assert
        await action.Should().ThrowExactlyAsync<MailBlusterHttpException>().Where(x => x.StatusCode == 404);
        _httpTest.ShouldHaveCalled($"*/leads/{RequestMd5}").WithHeader("Authorization", _options.Value.ApiKey);
    }

    [Fact]
    public async Task GetLeadAsync_WhenLeadDoesNotExist_ShouldReturnNull()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("NotFoundResponse.json", "Leads"), 404);
        const string RequestEmail = "noreply@sidio.nl";
        const string RequestMd5 = "949c658fa59ccb5a816400a4b0ad36f8";
        var client = CreateClient();

        // act
        var response = await client.GetLeadAsync(RequestEmail);

        // assert
        response.Should().BeNull();
        _httpTest.ShouldHaveCalled($"*/leads/{RequestMd5}").WithHeader("Authorization", _options.Value.ApiKey);
    }

    [Fact]
    public async Task DeleteLeadAsync_WhenRequestIsValid_ShouldReturnSuccess()
    {
        // arrange
        const string RequestEmail = "noreply@sidio.nl";
        const string RequestMd5 = "949c658fa59ccb5a816400a4b0ad36f8";
        _httpTest.RespondWith(ReadJsonData("DeleteResponse.json", "Leads"));
        var client = CreateClient();

        // act
        var response = await client.DeleteLeadAsync(RequestEmail);

        // assert
        response.Should().NotBeNull();
        response.LeadHash.Should().Be("5a91f0b2d2c1e5c3229d906d978b7337");
        response.Message.Should().Be("Lead deleted");
        response.Success.Should().BeTrue();
        _httpTest.ShouldHaveCalled($"*/leads/{RequestMd5}").WithHeader("Authorization", _options.Value.ApiKey);
    }

    [Fact]
    public async Task DeleteLeadAsync_WhenLeadDoesNotExist_ShouldReturnSuccessWithEmptyId()
    {
        // arrange
        const string RequestEmail = "noreply@sidio.nl";
        const string RequestMd5 = "949c658fa59ccb5a816400a4b0ad36f8";
        _httpTest.RespondWith(ReadJsonData("DeleteNotFoundResponse.json", "Leads"), 404);
        var client = CreateClient();

        // act
        var response = await client.DeleteLeadAsync(RequestEmail);

        // assert
        response.Should().NotBeNull();
        response.LeadHash.Should().BeNullOrEmpty();
        response.Message.Should().Be("Lead not found");
        response.Success.Should().BeFalse();
        _httpTest.ShouldHaveCalled($"*/leads/{RequestMd5}").WithHeader("Authorization", _options.Value.ApiKey);
    }
}