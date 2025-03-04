using System.Net;
using Sidio.MailBluster.Requests.Leads;

namespace Sidio.MailBluster.Tests;

public sealed partial class MailBlusterClientTests
{
    [Fact]
    public async Task CreateLeadAsync_WhenRequestIsValid_ShouldReturnLead()
    {
        // arrange
        var responseData = ReadJsonData("CreateResponse.json", "Leads");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When($"{BaseUrl}/{MailBlusterApiConstants.Leads}")
            .Respond(HttpStatusCode.Created, ApplicationJson, responseData);

        var client = CreateClient(mockHttp);

        var fields = new Dictionary<string, string> {{_fixture.Create<string>(), _fixture.Create<string>()}};
        var request = _fixture.Build<CreateLeadRequest>().With(x => x.Fields, fields).Create();

        // act
        var response = await client.CreateLeadAsync(request);

        // assert
        response.Should().NotBeNull();
        response.Message.Should().Be("Lead added");
        response.Lead!.Email.Should().Be("richard@example.com");
        response.Lead.Id.Should().Be(329395);
    }

    [Fact]
    public async Task CreateLeadAsync_UnprocessableEntity_ShouldReturnError()
    {
        // arrange
        var responseData = ReadJsonData("UnprocessableEntity.json", "Errors");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When($"{BaseUrl}/{MailBlusterApiConstants.Leads}")
            .Respond(HttpStatusCode.UnprocessableEntity, ApplicationJson, responseData);

        var client = CreateClient(mockHttp);

        var fields = new Dictionary<string, string> {{_fixture.Create<string>(), _fixture.Create<string>()}};
        var request = _fixture.Build<CreateLeadRequest>().With(x => x.Fields, fields).Create();

        // act
        var action = () => client.CreateLeadAsync(request);

        // assert
        await action.Should().ThrowExactlyAsync<MailBlusterUnprocessableEntityException>()
            .Where(
                x => x.UnprocessableEntityResponse["id"] == "Order ID is required" &&
                     x.UnprocessableEntityResponse["customer"] == "Customer is required");
    }

    [Fact]
    public async Task UpdateLeadAsync_WhenLeadExists_ShouldReturnLead()
    {
        // arrange
        const string RequestEmail = "noreply@sidio.nl";
        const string RequestMd5 = "949c658fa59ccb5a816400a4b0ad36f8";
        var responseData = ReadJsonData("UpdateResponse.json", "Leads");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When($"{BaseUrl}/{MailBlusterApiConstants.LeadsByHash.Replace("{lead_hash}", RequestMd5)}")
            .Respond(ApplicationJson, responseData);

        var client = CreateClient(mockHttp);

        var request = _fixture.Build<UpdateLeadRequest>()
            .With(x => x.Email, RequestEmail).Create();

        // act
        var response = await client.UpdateLeadAsync(RequestEmail, request);

        // assert
        response.Should().NotBeNull();
        response.Message.Should().Be("Lead updated");
        response.Lead!.Email.Should().Be("richard@example.com");
        response.Lead.Id.Should().Be(329395);
    }

    [Fact]
    public async Task GetLeadAsync_WhenRequestIsValid_ShouldReturnLead()
    {
        // arrange
        const string RequestEmail = "noreply@sidio.nl";
        const string RequestMd5 = "949c658fa59ccb5a816400a4b0ad36f8";
        var responseData = ReadJsonData("ReadResponse.json", "Leads");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When($"{BaseUrl}/{MailBlusterApiConstants.LeadsByHash.Replace("{lead_hash}", RequestMd5)}")
            .Respond(HttpStatusCode.Created, ApplicationJson, responseData);

        var client = CreateClient(mockHttp);

        // act
        var response = await client.GetLeadAsync(RequestEmail);

        // assert
        response.Should().NotBeNull();
        response.Email.Should().Be("richard@example.com");
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
    }

    [Fact]
    public async Task GetLeadAsync_WhenApiEndpointDoesNotExist_ShouldReturnError()
    {
        // arrange
        const string RequestEmail = "noreply@sidio.nl";
        const string RequestMd5 = "949c658fa59ccb5a816400a4b0ad36f8";
        var responseData = ReadJsonData("ApiEndpointDoesNotExist.json", "Errors");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When($"{BaseUrl}/{MailBlusterApiConstants.LeadsByHash.Replace("{lead_hash}", RequestMd5)}")
            .Respond(HttpStatusCode.NotFound, ApplicationJson, responseData);

        var client = CreateClient(mockHttp);

        // act
        var action = () => client.GetLeadAsync(RequestEmail);

        // assert
        await action.Should().ThrowExactlyAsync<MailBlusterHttpException>().Where(x => x.HttpStatusCode == HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetLeadAsync_WhenLeadDoesNotExist_ShouldReturnNull()
    {
        // arrange
        const string RequestEmail = "noreply@sidio.nl";
        const string RequestMd5 = "949c658fa59ccb5a816400a4b0ad36f8";
        var responseData = ReadJsonData("NotFoundResponse.json", "Leads");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When($"{BaseUrl}/{MailBlusterApiConstants.LeadsByHash.Replace("{lead_hash}", RequestMd5)}")
            .Respond(HttpStatusCode.NotFound, ApplicationJson, responseData);

        var client = CreateClient(mockHttp);

        // act
        var response = await client.GetLeadAsync(RequestEmail);

        // assert
        response.Should().BeNull();
    }

    [Fact]
    public async Task DeleteLeadAsync_WhenRequestIsValid_ShouldReturnSuccess()
    {
        // arrange
        const string RequestEmail = "noreply@sidio.nl";
        const string RequestMd5 = "949c658fa59ccb5a816400a4b0ad36f8";
        var responseData = ReadJsonData("DeleteResponse.json", "Leads");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When($"{BaseUrl}/{MailBlusterApiConstants.LeadsByHash.Replace("{lead_hash}", RequestMd5)}")
            .Respond(ApplicationJson, responseData);

        var client = CreateClient(mockHttp);

        // act
        var response = await client.DeleteLeadAsync(RequestEmail);

        // assert
        response.Should().NotBeNull();
        response.LeadHash.Should().Be("5a91f0b2d2c1e5c3229d906d978b7337");
        response.Message.Should().Be("Lead deleted");
        response.Success.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteLeadAsync_WhenLeadDoesNotExist_ShouldReturnSuccessWithEmptyId()
    {
        // arrange
        const string RequestEmail = "noreply@sidio.nl";
        const string RequestMd5 = "949c658fa59ccb5a816400a4b0ad36f8";
        var responseData = ReadJsonData("DeleteNotFoundResponse.json", "Leads");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When($"{BaseUrl}/{MailBlusterApiConstants.LeadsByHash.Replace("{lead_hash}", RequestMd5)}")
            .Respond(HttpStatusCode.NotFound, ApplicationJson, responseData);

        var client = CreateClient(mockHttp);

        // act
        var response = await client.DeleteLeadAsync(RequestEmail);

        // assert
        response.Should().NotBeNull();
        response.LeadHash.Should().BeNullOrEmpty();
        response.Message.Should().Be("Lead not found");
        response.Success.Should().BeFalse();
    }
}