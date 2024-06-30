using Xunit.Abstractions;

namespace Sidio.MailBluster.Integration.Tests.Hooks;

[Binding]
public sealed class CleanupLeadHook
{
    internal const string ContextKey = "lead-email";

    private readonly IMailBlusterClient _client;

    public CleanupLeadHook(IMailBlusterClient client)
    {
        _client = client;
    }

    [AfterScenario("cleanupLead")]
    public async Task CleanupAsync(ScenarioContext scenarioContext)
    {
        var testOutputHelper = GetTestOutputHelper(scenarioContext);
        var email = scenarioContext.Get<string>(ContextKey);
        if (!string.IsNullOrWhiteSpace(email))
        {
            var result = await _client.DeleteLeadAsync(email);
            testOutputHelper.WriteLine($"Trying to delete lead with email `{email}`: {result.Message}");
        }
        else
        {
            testOutputHelper.WriteLine("No email found in scenario context, skipping cleanup");
        }
    }

    private static ITestOutputHelper GetTestOutputHelper(ScenarioContext scenarioContext) => scenarioContext.ScenarioContainer.Resolve<ITestOutputHelper>();
}