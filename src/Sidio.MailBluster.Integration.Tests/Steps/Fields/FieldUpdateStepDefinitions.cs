using Sidio.MailBluster.Integration.Tests.Drivers.Fields;
using Sidio.MailBluster.Integration.Tests.Hooks;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Integration.Tests.Steps.Fields;

[Binding]
[Scope(Scenario = "Update a field")]
[Collection("ReqnrollNonParallelizableFeatures")]
public sealed class FieldUpdateStepDefinitions
{
    private readonly FieldExistsDriver _fieldExistsDriver;
    private readonly UpdateFieldDriver _updateFieldDriver;
    private readonly ScenarioContext _scenarioContext;

    private Field? _field;
    private string? _fieldLabel;

    public FieldUpdateStepDefinitions(
        FieldExistsDriver fieldExistsDriver,
        UpdateFieldDriver updateFieldDriver,
        ScenarioContext scenarioContext)
    {
        _fieldExistsDriver = fieldExistsDriver;
        _updateFieldDriver = updateFieldDriver;
        _scenarioContext = scenarioContext;
    }

    private Field Field => _field ?? throw new InvalidOperationException("Field is not set");

    [Given(@"a field exists")]
    public async Task GivenAFieldExists()
    {
        var field = _scenarioContext.GetField() ?? throw new InvalidOperationException("Field is not set in scenario context");
        _field = await _fieldExistsDriver.FieldShouldExistAsync(field.FieldLabel);
    }

    [When(@"the field is updated")]
    public async Task WhenTheFieldIsUpdated()
    {
        _fieldLabel = FieldHelper.CreateValidFieldLabel();
        await _updateFieldDriver.UpdateFieldAsync(Field.Id!.Value, _fieldLabel, _fieldLabel);
    }

    [Then(@"the field should be updated")]
    public async Task ThenTheFieldShouldBeUpdated()
    {
        var result = await _fieldExistsDriver.FieldShouldExistAsync(_fieldLabel!);
        result.Should().NotBeNull();
    }
}