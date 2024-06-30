using Sidio.MailBluster.Models;
using Sidio.MailBluster.Requests.Leads;

namespace Sidio.MailBluster.Integration.Tests.Drivers;

[Binding]
public sealed class CreateLeadDriver
{
    private readonly IMailBlusterClient _client;

    public CreateLeadDriver(IMailBlusterClient client)
    {
        _client = client;
    }

    public async Task<Lead> CreateLeadAsync(string email)
    {
        var createLeadRequest = new CreateLeadRequest
        {
            Email = email,
            Subscribed = false,
        };

        var result = await _client.CreateLeadAsync(createLeadRequest);
        result.Should().NotBeNull();
        result.Lead.Should().NotBeNull();
        result.Lead!.Email.Should().Be(email);

        return result.Lead;
    }
}