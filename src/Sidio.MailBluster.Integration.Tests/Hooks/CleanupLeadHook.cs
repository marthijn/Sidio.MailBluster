using Sidio.MailBluster.Integration.Tests.Repositories;
using Sidio.MailBluster.Models;
using Xunit.Abstractions;

namespace Sidio.MailBluster.Integration.Tests.Hooks;

[Binding]
public sealed class CleanupLeadHook
{
    internal const string ContextKey = "lead-email";

    private readonly LeadRepository _repository;

    public CleanupLeadHook(LeadRepository repository)
    {
        _repository = repository;
    }

    [AfterScenario("cleanupLead")]
    public async Task CleanupAsync(ScenarioContext scenarioContext)
    {
        var testOutputHelper = GetTestOutputHelper(scenarioContext);

        var deleteResult = false;
        if (scenarioContext.TryGetValue(ContextKey, out string email) && !string.IsNullOrWhiteSpace(email))
        {
            deleteResult = await DeleteLead(email, testOutputHelper);
        }

        if (scenarioContext.TryGetValue(CreateLeadHook.ContextKey, out Lead lead) && lead != null)
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

    private static ITestOutputHelper GetTestOutputHelper(ScenarioContext scenarioContext) => scenarioContext.ScenarioContainer.Resolve<ITestOutputHelper>();
}