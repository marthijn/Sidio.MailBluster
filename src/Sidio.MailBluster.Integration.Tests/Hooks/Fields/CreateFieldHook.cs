using Sidio.MailBluster.Integration.Tests.Repositories;
using Sidio.MailBluster.Requests.Fields;

namespace Sidio.MailBluster.Integration.Tests.Hooks.Fields;

[Binding]
public sealed class CreateFieldHook
{
    private readonly Fixture _fixture = new ();
    private readonly FieldRepository _repository;

    public CreateFieldHook(FieldRepository repository)
    {
        _repository = repository;
    }

    [BeforeScenario("createField")]
    public async Task CreateFieldAsync(ScenarioContext scenarioContext)
    {
        var testOutputHelper = scenarioContext.GetTestOutputHelper();
        var label = _fixture.Create<string>();
        var result = await _repository.CreateAsync(
            new CreateFieldRequest
            {
                FieldLabel = label,
                FieldMergeTag = label.ToLower()
            });

        result.Should().NotBeNull();
        result.Field.Should().NotBeNull();
        result.Field!.FieldLabel.Should().Be(label);
        result.Field.FieldMergeTag.Should().Be(label.ToLower());

        testOutputHelper.WriteLine($"Created field with label `{label}` and id `{result.Field.Id}`");

        scenarioContext.SetField(result.Field);
    }
}