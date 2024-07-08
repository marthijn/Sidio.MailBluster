using Sidio.MailBluster.Integration.Tests.Repositories;
using Sidio.MailBluster.Requests.Leads;

namespace Sidio.MailBluster.Integration.Tests.Hooks.Leads;

[Binding]
public sealed class CreateLeadHook
{
    private readonly Fixture _fixture = new ();
    private readonly LeadRepository _repository;

    public CreateLeadHook(LeadRepository repository)
    {
        _repository = repository;
    }

    [BeforeScenario("createLead")]
    public async Task CreateLeadAsync(ScenarioContext scenarioContext)
    {
        var testOutputHelper = scenarioContext.GetTestOutputHelper();
        var email = $"{_fixture.Create<string>()}@example.com";
        var result = await _repository.CreateAsync(
            new CreateLeadRequest
            {
                Email = email,
                Subscribed = false,
            });

        result.Should().NotBeNull();
        result.Lead.Should().NotBeNull();
        result.Lead!.Email.Should().Be(email);

        testOutputHelper.WriteLine($"Created lead with email `{email}` and id `{result.Lead.Id}`");

        scenarioContext.SetLead(result.Lead);
    }
}