using Sidio.MailBluster.Integration.Tests.Drivers.Fields;
using Sidio.MailBluster.Integration.Tests.Hooks;

namespace Sidio.MailBluster.Integration.Tests.Steps.Fields;

[Binding]
[Scope(Scenario = "Create a field")]
public sealed class FieldCreateStepDefinitions
{
    private readonly FieldExistsDriver _fieldExistsDriver;
    private readonly CreateFieldDriver _createFieldDriver;
    private readonly ScenarioContext _scenarioContext;

    private string? _label;

    public FieldCreateStepDefinitions(
        FieldExistsDriver fieldExistsDriver,
        CreateFieldDriver createFieldDriver,
        ScenarioContext scenarioContext)
    {
        _fieldExistsDriver = fieldExistsDriver;
        _createFieldDriver = createFieldDriver;
        _scenarioContext = scenarioContext;
    }

    private string Label => _label ?? throw new InvalidOperationException("Label is not set");

    [BeforeScenario]
    public void BeforeScenario()
    {
        // Field merge tag can contain letter, number & underscore only
        _label = FieldHelper.CreateValidFieldLabel();
    }

    [Given(@"a field does not exist")]
    public async Task GivenAFieldDoesNotExist()
    {
        await _fieldExistsDriver.FieldShouldNotExistAsync(Label);
    }

    [When(@"the field is created")]
    public async Task WhenTheFieldIsCreated()
    {
        var result = await _createFieldDriver.CreateFieldAsync(Label);
        _scenarioContext.SetFieldId(result.Id!.Value);
    }

    [Then(@"the field should exist")]
    public async Task ThenTheFieldShouldExist()
    {
        await _fieldExistsDriver.FieldShouldExistAsync(Label);
    }
}