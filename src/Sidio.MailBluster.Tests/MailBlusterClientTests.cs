using System.Reflection;
using Flurl.Http.Configuration;
using Flurl.Http.Testing;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Sidio.MailBluster.Models;
using Sidio.MailBluster.Requests.Leads;

namespace Sidio.MailBluster.Tests;

public sealed class MailBlusterClientTests : IDisposable
{
    private readonly HttpTest _httpTest = new ();

    private readonly IOptions<MailBlusterOptions> _options =
        Options.Create(new MailBlusterOptions { ApiKey = "key1", Url = "https://localhost/api/" });

    private readonly Fixture _fixture = new ();

    public void Dispose()
    {
        _httpTest.Dispose();
    }

    [Fact]
    public async Task CreateLeadAsync_WhenRequestIsValid_ShouldReturnLead()
    {
        // arrange
        _httpTest.RespondWith(ReadJsonData("CreateResponse.json", "Leads"));

        var fields = new Fields();
        fields.AddField(_fixture.Create<string>(), _fixture.Create<string>());

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
    public async Task GetLeadAsync_WhenLeadDoesNotExist_ShouldReturnNull()
    {
        // arrange
        _httpTest.RespondWith(status: 404);
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
        _httpTest.ShouldHaveCalled($"*/leads/{RequestMd5}").WithHeader("Authorization", _options.Value.ApiKey);
    }

    private MailBlusterClient CreateClient() => new(new FlurlClientCache(), _options, NullLogger<MailBlusterClient>.Instance);

    private static string ReadJsonData(string filename, string section)
    {
        var assembly = typeof(MailBlusterClientTests).GetTypeInfo().Assembly;
        using var resource =
            assembly.GetManifestResourceStream($"Sidio.MailBluster.Tests.Json.{section}.{filename}");
        using var reader = new StreamReader(resource!);
        var result = reader.ReadToEnd();
        return result;
    }
}