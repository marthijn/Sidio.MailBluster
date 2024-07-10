using Sidio.MailBluster.Integration.Tests.Repositories;
using Sidio.MailBluster.Models;
using Sidio.MailBluster.Requests.Fields;

namespace Sidio.MailBluster.Integration.Tests.Drivers.Fields;

[Binding]
public sealed class UpdateFieldDriver
{
    private readonly FieldRepository _repository;

    public UpdateFieldDriver(FieldRepository repository)
    {
        _repository = repository;
    }

    public async Task<Field> UpdateFieldAsync(long id, string label, string fieldMergeTag)
    {
        var request = new UpdateFieldRequest
        {
            FieldLabel = label,
            FieldMergeTag = fieldMergeTag,
        };

        var result = await _repository.UpdateAsync(id, request);
        result.Should().NotBeNull();
        result.Field.Should().NotBeNull();
        result.Field!.FieldLabel.Should().Be(label);

        return result.Field;
    }
}