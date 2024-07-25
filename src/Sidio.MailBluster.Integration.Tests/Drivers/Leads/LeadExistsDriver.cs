using Polly.Retry;
using Sidio.MailBluster.Integration.Tests.Repositories;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Integration.Tests.Drivers.Leads;

[Binding]
public sealed class LeadExistsDriver
{
    private readonly LeadRepository _repository;
    private readonly AsyncRetryPolicy _retryPolicy;

    public LeadExistsDriver(LeadRepository repository, AsyncRetryPolicy retryPolicy)
    {
        _repository = repository;
        _retryPolicy = retryPolicy;
    }

    public async Task LeadShouldNotExistAsync(string email)
    {
        var result = await _repository.GetAsync(email);
        result.Should().BeNull();
    }

    public async Task<Lead> LeadShouldExistAsync(string email, string? lastNameShouldBe = null)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var result = await _repository.GetAsync(email);
            result.Should().NotBeNull();
            result!.Email.Should().Be(email);

            if (lastNameShouldBe is not null)
            {
                result.LastName.Should().Be(lastNameShouldBe);
            }

            return result;
        });
    }
}