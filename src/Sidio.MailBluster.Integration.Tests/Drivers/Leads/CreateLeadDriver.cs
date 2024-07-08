using Sidio.MailBluster.Integration.Tests.Repositories;
using Sidio.MailBluster.Models;
using Sidio.MailBluster.Requests.Leads;

namespace Sidio.MailBluster.Integration.Tests.Drivers.Leads;

[Binding]
public sealed class CreateLeadDriver
{
    private readonly LeadRepository _repository;

    public CreateLeadDriver(LeadRepository repository)
    {
        _repository = repository;
    }

    public async Task<Lead> CreateLeadAsync(string email)
    {
        var createLeadRequest = new CreateLeadRequest
        {
            Email = email,
            Subscribed = false,
        };

        var result = await _repository.CreateAsync(createLeadRequest);
        result.Should().NotBeNull();
        result.Lead.Should().NotBeNull();
        result.Lead!.Email.Should().Be(email);

        return result.Lead;
    }
}