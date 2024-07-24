using Sidio.MailBluster.Integration.Tests.Context;
using Sidio.MailBluster.Integration.Tests.Drivers.Leads;

namespace Sidio.MailBluster.Integration.Tests.Steps;

[Binding]
[Scope(Tag = "leads")]
[Collection("ReqnrollNonParallelizableFeatures")]
public sealed class LeadsStepDefinitions
{
    private readonly Fixture _fixture = new ();
    private readonly LeadContext _leadContext;
    private readonly LeadExistsDriver _leadExistsDriver;
    private readonly CreateLeadDriver _createLeadDriver;
    private readonly DeleteLeadDriver _deleteLeadDriver;
    private readonly UpdateLeadDriver _updateLeadDriver;

    public LeadsStepDefinitions(
        LeadContext leadContext,
        LeadExistsDriver leadExistsDriver,
        CreateLeadDriver createLeadDriver,
        DeleteLeadDriver deleteLeadDriver,
        UpdateLeadDriver updateLeadDriver)
    {
        _leadContext = leadContext;
        _leadExistsDriver = leadExistsDriver;
        _createLeadDriver = createLeadDriver;
        _deleteLeadDriver = deleteLeadDriver;
        _updateLeadDriver = updateLeadDriver;
    }

    private string Email => _leadContext.Email ?? throw new InvalidOperationException("Email is not set");

    [Given(@"a lead does not exist")]
    public async Task GivenALeadDoesNotExist()
    {
        await _leadExistsDriver.LeadShouldNotExistAsync(Email);
    }

    [Given(@"a lead exists")]
    public async Task GivenALeadExists()
    {
        await _createLeadDriver.CreateLeadAsync(Email);
    }

    [When(@"the lead is created")]
    public async Task WhenTheLeadIsCreated()
    {
        var result = await _createLeadDriver.CreateLeadAsync(Email);
        _leadContext.Email = result.Email;
    }

    [When(@"the lead is deleted")]
    public async Task WhenTheLeadIsDeleted()
    {
        await _deleteLeadDriver.DeleteLeadAsync(Email);
    }

    [When(@"the lead is updated")]
    public async Task WhenTheLeadIsUpdated()
    {
        _leadContext.LastName = _fixture.Create<string>();
        await _updateLeadDriver.UpdateLeadAsync(Email, _leadContext.LastName);
    }

    [Then(@"the lead should exist")]
    public async Task ThenTheLeadShouldExist()
    {
        await _leadExistsDriver.LeadShouldExistAsync(Email);
    }

    [Then(@"the lead should not exist")]
    public async Task ThenTheLeadShouldNotExist()
    {
        await _leadExistsDriver.LeadShouldNotExistAsync(Email);
        _leadContext.Email = null;
    }

    [Then(@"the lead should be updated")]
    public async Task ThenTheLeadShouldBeUpdated()
    {
        await _leadExistsDriver.LeadShouldExistAsync(
            Email,
            _leadContext.LastName ?? throw new InvalidOperationException("Last name is not set"));
    }
}