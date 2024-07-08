using Sidio.MailBluster.Integration.Tests.Repositories;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Integration.Tests.Drivers.Leads;

[Binding]
public sealed class LeadExistsDriver
{
    private readonly LeadRepository _repository;

    public LeadExistsDriver(LeadRepository repository)
    {
        _repository = repository;
    }

    public async Task LeadShouldNotExistAsync(string email)
    {
        var result = await _repository.GetAsync(email);
        result.Should().BeNull();
    }

    public async Task<Lead> LeadShouldExistAsync(string email)
    {
        var result = await _repository.GetAsync(email);
        result.Should().NotBeNull();
        result!.Email.Should().Be(email);
        return result;
    }
}