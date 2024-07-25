using Sidio.MailBluster.Integration.Tests.Context;
using Sidio.MailBluster.Integration.Tests.Repositories;
using Xunit.Abstractions;

namespace Sidio.MailBluster.Integration.Tests.Hooks.Fields;

[Binding]
public sealed class CleanupFieldHook
{
    private readonly FieldRepository _repository;
    private readonly FieldContext _fieldContext;

    public CleanupFieldHook(FieldRepository repository, FieldContext fieldContext)
    {
        _repository = repository;
        _fieldContext = fieldContext;
    }

    [AfterScenario("fields")]
    public async Task CleanupAsync(ScenarioContext scenarioContext)
    {
        var testOutputHelper = scenarioContext.GetTestOutputHelper();
        var deleteResult = false;

        if (_fieldContext.Field?.Id != null)
        {
            deleteResult = await DeleteFieldAsync(_fieldContext.Field.Id.Value, testOutputHelper);
        }

        if (!deleteResult)
        {
            testOutputHelper.WriteLine("No field found in field context, skipping cleanup");
        }
    }

    private async Task<bool> DeleteFieldAsync(long id, ITestOutputHelper testOutputHelper)
    {
        var result = await _repository.DeleteAsync(id);
        testOutputHelper.WriteLine($"Trying to delete field with id `{id}`: {result.Message}");
        return result.Id is > 0;
    }
}