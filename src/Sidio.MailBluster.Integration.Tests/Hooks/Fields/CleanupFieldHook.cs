using Sidio.MailBluster.Integration.Tests.Repositories;
using Xunit.Abstractions;

namespace Sidio.MailBluster.Integration.Tests.Hooks.Fields;

[Binding]
public sealed class CleanupFieldHook
{
    private readonly FieldRepository _repository;

    public CleanupFieldHook(FieldRepository repository)
    {
        _repository = repository;
    }

    [AfterScenario("cleanupField")]
    public async Task CleanupAsync(ScenarioContext scenarioContext)
    {
        var testOutputHelper = scenarioContext.GetTestOutputHelper();
        var deleteResult = false;

        var id = scenarioContext.GetFieldId();
        if (id.HasValue)
        {
            deleteResult = await DeleteFieldAsync(id.Value, testOutputHelper);
        }

        var field = scenarioContext.GetField();
        if (field?.Id != null)
        {
            deleteResult = await DeleteFieldAsync(field.Id.Value, testOutputHelper);
        }

        if (!deleteResult)
        {
            testOutputHelper.WriteLine("No field found in scenario context, skipping cleanup");
        }
    }

    private async Task<bool> DeleteFieldAsync(long id, ITestOutputHelper testOutputHelper)
    {
        var result = await _repository.DeleteAsync(id);
        testOutputHelper.WriteLine($"Trying to delete field with id `{id}`: {result.Message}");
        return result.Id is > 0;
    }
}