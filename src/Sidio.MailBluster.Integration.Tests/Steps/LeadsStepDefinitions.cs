using Sidio.MailBluster.Integration.Tests.Drivers;
using Sidio.MailBluster.Integration.Tests.Hooks;

namespace Sidio.MailBluster.Integration.Tests.Steps;

[Binding]
public sealed class LeadsStepDefinitions
{
    private readonly Fixture _fixture = new ();
    private readonly LeadExistsDriver _leadExistsDriver;
    private readonly CreateLeadDriver _createLeadDriver;
    private readonly ScenarioContext _scenarioContext;

    private string? _email;

    public LeadsStepDefinitions(
        LeadExistsDriver leadExistsDriver,
        CreateLeadDriver createLeadDriver,
        ScenarioContext scenarioContext)
    {
        _leadExistsDriver = leadExistsDriver;
        _createLeadDriver = createLeadDriver;
        _scenarioContext = scenarioContext;
    }

    private string Email => _email ?? throw new InvalidOperationException("Email is not set");

    [BeforeScenario]
    public void BeforeScenario()
    {
        _email = $"{_fixture.Create<string>()}@example.com";
        _scenarioContext.Set(_email, CleanupLeadHook.ContextKey);
    }

    [Given(@"a lead does not exist")]
    public async Task GivenALeadDoesNotExist()
    {
        await _leadExistsDriver.LeadShouldNotExistAsync(Email);
    }

    [When(@"the lead is created")]
    public async Task WhenTheLeadIsCreated()
    {
        await _createLeadDriver.CreateLeadAsync(Email);
    }

    [Then(@"the lead should exist")]
    public async Task ThenTheLeadShouldExist()
    {
        await _leadExistsDriver.LeadShouldExistAsync(Email);
    }
}