using Sidio.MailBluster.Integration.Tests.Context;
using Sidio.MailBluster.Integration.Tests.Repositories;
using Xunit.Abstractions;

namespace Sidio.MailBluster.Integration.Tests.Hooks.Leads;

[Binding]
public sealed class CleanupLeadHook
{
    private readonly LeadRepository _repository;
    private readonly LeadContext _leadContext;

    public CleanupLeadHook(LeadRepository repository, LeadContext leadContext)
    {
        _repository = repository;
        _leadContext = leadContext;
    }

    [AfterScenario("leads")]
    public async Task CleanupAsync(ScenarioContext scenarioContext)
    {
        var testOutputHelper = scenarioContext.GetTestOutputHelper();
        var deleteResult = false;

        if (!string.IsNullOrWhiteSpace(_leadContext.Email))
        {
            deleteResult = await DeleteLead(_leadContext.Email, testOutputHelper);
        }

        if (!deleteResult)
        {
            testOutputHelper.WriteLine("No email found in lead context, skipping cleanup");
        }
    }

    private async Task<bool> DeleteLead(string email, ITestOutputHelper testOutputHelper)
    {
        var result = await _repository.DeleteAsync(email);
        testOutputHelper.WriteLine($"Trying to delete lead with email `{email}`: {result.Message}");
        return !string.IsNullOrWhiteSpace(result.LeadHash);
    }
}