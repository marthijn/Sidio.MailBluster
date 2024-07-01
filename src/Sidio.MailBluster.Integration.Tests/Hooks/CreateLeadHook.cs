using Sidio.MailBluster.Integration.Tests.Repositories;
using Sidio.MailBluster.Requests.Leads;
using Xunit.Abstractions;

namespace Sidio.MailBluster.Integration.Tests.Hooks;

[Binding]
public sealed class CreateLeadHook
{
    internal const string ContextKey = "lead";

    private readonly Fixture _fixture = new ();
    private readonly LeadRepository _repository;

    public CreateLeadHook(LeadRepository repository)
    {
        _repository = repository;
    }

    [BeforeScenario("createLead")]
    public async Task CreateLeadAsync(ScenarioContext scenarioContext)
    {
        var testOutputHelper = GetTestOutputHelper(scenarioContext);
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

        testOutputHelper.WriteLine($"Created lead with email `{email}`");

        scenarioContext.Set(result.Lead, ContextKey);
    }

    private static ITestOutputHelper GetTestOutputHelper(ScenarioContext scenarioContext) => scenarioContext.ScenarioContainer.Resolve<ITestOutputHelper>();
}