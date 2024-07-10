using Sidio.MailBluster.Integration.Tests.Drivers.Fields;
using Sidio.MailBluster.Integration.Tests.Hooks;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Integration.Tests.Steps.Fields;

[Binding]
[Scope(Scenario = "Delete a field")]
[Collection("ReqnrollNonParallelizableFeatures")]
public sealed class FieldDeleteStepDefinitions
{
    private readonly DeleteFieldDriver _deleteFieldDriver;
    private readonly FieldExistsDriver _fieldExistsDriver;
    private readonly ScenarioContext _scenarioContext;

    private Field? _field;

    public FieldDeleteStepDefinitions(
        DeleteFieldDriver deleteFieldDriver,
        FieldExistsDriver fieldExistsDriver,
        ScenarioContext scenarioContext)
    {
        _deleteFieldDriver = deleteFieldDriver;
        _fieldExistsDriver = fieldExistsDriver;
        _scenarioContext = scenarioContext;
    }

    private Field Field => _field ?? throw new InvalidOperationException("Field is not set");

    [Given(@"a field exists")]
    public async Task GivenAFieldExists()
    {
        var field = _scenarioContext.GetField() ?? throw new InvalidOperationException("Field is not set in scenario context");
        _field = await _fieldExistsDriver.FieldShouldExistAsync(field.FieldLabel);
    }

    [When(@"the field is deleted")]
    public async Task WhenTheFieldIsDeleted()
    {
        await _deleteFieldDriver.DeleteFieldAsync(Field.Id!.Value);
    }

    [Then(@"the field should not exist")]
    public async Task ThenTheFieldShouldNotExist()
    {
        await _fieldExistsDriver.FieldShouldNotExistAsync(Field.FieldLabel);
    }
}