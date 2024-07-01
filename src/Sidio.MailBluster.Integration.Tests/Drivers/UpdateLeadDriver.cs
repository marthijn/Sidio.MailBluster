using Sidio.MailBluster.Integration.Tests.Repositories;
using Sidio.MailBluster.Models;
using Sidio.MailBluster.Requests.Leads;

namespace Sidio.MailBluster.Integration.Tests.Drivers;

[Binding]
public sealed class UpdateLeadDriver
{
    private readonly LeadRepository _repository;

    public UpdateLeadDriver(LeadRepository repository)
    {
        _repository = repository;
    }

    public async Task<Lead> UpdateLeadAsync(string email, string updatedLastName)
    {
        var result = await _repository.UpdateAsync(
            email,
            new UpdateLeadRequest
            {
                Email = email,
                Subscribed = false,
                LastName = updatedLastName,
            });

        result.Should().NotBeNull();
        result.Lead.Should().NotBeNull();
        result.Lead!.LastName.Should().Be(updatedLastName);

        return result.Lead;
    }
}