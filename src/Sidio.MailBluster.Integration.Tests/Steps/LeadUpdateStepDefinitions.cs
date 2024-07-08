using Sidio.MailBluster.Integration.Tests.Drivers.Leads;
using Sidio.MailBluster.Integration.Tests.Hooks;

namespace Sidio.MailBluster.Integration.Tests.Steps;

[Binding]
[Scope(Scenario = "Update a lead")]
public sealed class LeadUpdateStepDefinitions
{
    private readonly Fixture _fixture = new ();
    private readonly UpdateLeadDriver _updateLeadDriver;
    private readonly LeadExistsDriver _leadExistsDriver;
    private readonly ScenarioContext _scenarioContext;

    private string? _email;
    private string? _lastName;

    public LeadUpdateStepDefinitions(
        UpdateLeadDriver updateLeadDriver,
        LeadExistsDriver leadExistsDriver,
        ScenarioContext scenarioContext)
    {
        _updateLeadDriver = updateLeadDriver;
        _leadExistsDriver = leadExistsDriver;
        _scenarioContext = scenarioContext;
    }

    private string Email => _email ?? throw new InvalidOperationException("Email is not set");

    [Given(@"a lead exists")]
    public async Task GivenALeadExists()
    {
        var lead = _scenarioContext.GetLead() ?? throw new InvalidOperationException("Lead is not set");
        await _leadExistsDriver.LeadShouldExistAsync(lead.Email);
        _email = lead.Email;
    }

    [When(@"the lead is updated")]
    public async Task WhenTheLeadIsUpdated()
    {
        _lastName = _fixture.Create<string>();
        await _updateLeadDriver.UpdateLeadAsync(Email, _lastName);
    }

    [Then(@"the lead should be updated")]
    public async Task ThenTheLeadShouldBeUpdated()
    {
        var result = await _leadExistsDriver.LeadShouldExistAsync(Email);
        result.LastName.Should().Be(_lastName);
    }
}