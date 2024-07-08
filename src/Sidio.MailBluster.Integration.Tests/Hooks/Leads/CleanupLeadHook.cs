using Sidio.MailBluster.Integration.Tests.Repositories;
using Xunit.Abstractions;

namespace Sidio.MailBluster.Integration.Tests.Hooks.Leads;

[Binding]
public sealed class CleanupLeadHook
{
    private readonly LeadRepository _repository;

    public CleanupLeadHook(LeadRepository repository)
    {
        _repository = repository;
    }

    [AfterScenario("cleanupLead")]
    public async Task CleanupAsync(ScenarioContext scenarioContext)
    {
        var testOutputHelper = scenarioContext.GetTestOutputHelper();

        var deleteResult = false;

        var email = scenarioContext.GetLeadEmail();
        if (!string.IsNullOrWhiteSpace(email))
        {
            deleteResult = await DeleteLead(email, testOutputHelper);
        }

        var lead = scenarioContext.GetLead();
        if (lead != null)
        {
            deleteResult = await DeleteLead(lead.Email, testOutputHelper);
        }

        if (!deleteResult)
        {
            testOutputHelper.WriteLine("No email or lead found in scenario context, skipping cleanup");
        }
    }

    private async Task<bool> DeleteLead(string email, ITestOutputHelper testOutputHelper)
    {
        var result = await _repository.DeleteAsync(email);
        testOutputHelper.WriteLine($"Trying to delete lead with email `{email}`: {result.Message}");
        return !string.IsNullOrWhiteSpace(result.LeadHash);
    }
}