using Sidio.MailBluster.Integration.Tests.Drivers.Leads;
using Sidio.MailBluster.Integration.Tests.Hooks;

namespace Sidio.MailBluster.Integration.Tests.Steps.Leads;

[Binding]
[Scope(Scenario = "Create a lead")]
[Collection("ReqnrollNonParallelizableFeatures")]
public sealed class LeadCreateStepDefinitions
{
    private readonly Fixture _fixture = new ();
    private readonly LeadExistsDriver _leadExistsDriver;
    private readonly CreateLeadDriver _createLeadDriver;
    private readonly ScenarioContext _scenarioContext;

    private string? _email;

    public LeadCreateStepDefinitions(
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
        _scenarioContext.SetLeadEmail(_email);
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