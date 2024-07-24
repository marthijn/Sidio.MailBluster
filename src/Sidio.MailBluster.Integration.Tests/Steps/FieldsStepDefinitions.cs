using Sidio.MailBluster.Integration.Tests.Context;
using Sidio.MailBluster.Integration.Tests.Drivers.Fields;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Integration.Tests.Steps;

[Binding]
[Scope(Tag = "fields")]
[Collection("ReqnrollNonParallelizableFeatures")]
public sealed class FieldsStepDefinitions
{
    private readonly FieldContext _fieldContext;
    private readonly CreateFieldDriver _createFieldDriver;
    private readonly FieldExistsDriver _fieldExistsDriver;
    private readonly UpdateFieldDriver _updateFieldDriver;
    private readonly DeleteFieldDriver _deleteFieldDriver;

    public FieldsStepDefinitions(
        FieldContext fieldContext,
        CreateFieldDriver createFieldDriver,
        FieldExistsDriver fieldExistsDriver,
        UpdateFieldDriver updateFieldDriver,
        DeleteFieldDriver deleteFieldDriver)
    {
        _fieldContext = fieldContext;
        _createFieldDriver = createFieldDriver;
        _fieldExistsDriver = fieldExistsDriver;
        _updateFieldDriver = updateFieldDriver;
        _deleteFieldDriver = deleteFieldDriver;
    }

    private string Label => _fieldContext.Label ?? throw new InvalidOperationException("Label is not set");

    private Field Field => _fieldContext.Field ?? throw new InvalidOperationException("Field is not set");

    [Given(@"a field does not exist")]
    public async Task GivenAFieldDoesNotExist()
    {
        await _fieldExistsDriver.FieldShouldNotExistAsync(Label);
    }

    [Given(@"a field exists")]
    public async Task GivenAFieldExists()
    {
        _fieldContext.Field = await _createFieldDriver.CreateFieldAsync(Label);
    }

    [When(@"the field is created")]
    public async Task WhenTheFieldIsCreated()
    {
        _fieldContext.Field = await _createFieldDriver.CreateFieldAsync(Label);
    }

    [When(@"the field is deleted")]
    public async Task WhenTheFieldIsDeleted()
    {
        await _deleteFieldDriver.DeleteFieldAsync(Field.Id!.Value);
    }

    [When(@"the field is updated")]
    public async Task WhenTheFieldIsUpdated()
    {
        _fieldContext.Label = FieldHelper.CreateValidFieldLabel();
        await _updateFieldDriver.UpdateFieldAsync(Field.Id!.Value, _fieldContext.Label, _fieldContext.Label);
    }

    [Then(@"the field should exist")]
    public async Task ThenTheFieldShouldExist()
    {
        await _fieldExistsDriver.FieldShouldExistAsync(Label);
    }

    [Then(@"the field should not exist")]
    public async Task ThenTheFieldShouldNotExist()
    {
        await _fieldExistsDriver.FieldShouldNotExistAsync(Field.FieldLabel);
        _fieldContext.Field = null;
    }

    [Then(@"the field should be updated")]
    public async Task ThenTheFieldShouldBeUpdated()
    {
        var result = await _fieldExistsDriver.FieldShouldExistAsync(_fieldContext.Label!);
        result.Should().NotBeNull();
    }
}