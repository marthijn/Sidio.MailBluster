using Sidio.MailBluster.Integration.Tests.Drivers.Leads;
using Sidio.MailBluster.Integration.Tests.Hooks;

namespace Sidio.MailBluster.Integration.Tests.Steps.Leads;

[Binding]
[Scope(Scenario = "Delete a lead")]
[Collection("ReqnrollNonParallelizableFeatures")]
public sealed class LeadDeleteStepDefinitions
{
    private readonly DeleteLeadDriver _deleteLeadDriver;
    private readonly LeadExistsDriver _leadExistsDriver;
    private readonly ScenarioContext _scenarioContext;

    private string? _email;

    public LeadDeleteStepDefinitions(
        DeleteLeadDriver deleteLeadDriver,
        LeadExistsDriver leadExistsDriver,
        ScenarioContext scenarioContext)
    {
        _deleteLeadDriver = deleteLeadDriver;
        _leadExistsDriver = leadExistsDriver;
        _scenarioContext = scenarioContext;
    }

    private string Email => _email ?? throw new InvalidOperationException("Email is not set");

    [Given(@"a lead exists")]
    public async Task GivenALeadExists()
    {
        var lead = _scenarioContext.GetLead() ?? throw new InvalidOperationException("Lead is not set in scenario context");
        await _leadExistsDriver.LeadShouldExistAsync(lead.Email);
        _email = lead.Email;
    }

    [When(@"the lead is deleted")]
    public async Task WhenTheLeadIsDeleted()
    {
        await _deleteLeadDriver.DeleteLeadAsync(Email);
    }

    [Then(@"the lead should not exist")]
    public async Task ThenTheLeadShouldNotExist()
    {
        await _leadExistsDriver.LeadShouldNotExistAsync(Email);
    }
}