using Sidio.MailBluster.Integration.Tests.Repositories;

namespace Sidio.MailBluster.Integration.Tests.Drivers;

[Binding]
public sealed class DeleteLeadDriver
{
    private readonly LeadRepository _repository;

    public DeleteLeadDriver(LeadRepository repository)
    {
        _repository = repository;
    }

    public async Task DeleteLeadAsync(string email)
    {
        var result = await _repository.DeleteAsync(email);
        result.Should().NotBeNull();
    }
}