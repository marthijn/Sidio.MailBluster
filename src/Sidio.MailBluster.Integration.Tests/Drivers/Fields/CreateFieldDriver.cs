using Sidio.MailBluster.Integration.Tests.Repositories;
using Sidio.MailBluster.Models;
using Sidio.MailBluster.Requests.Fields;

namespace Sidio.MailBluster.Integration.Tests.Drivers.Fields;

[Binding]
public sealed class CreateFieldDriver
{
    private readonly FieldRepository _repository;

    public CreateFieldDriver(FieldRepository repository)
    {
        _repository = repository;
    }

    public async Task<Field> CreateFieldAsync(string label)
    {
        var result = await _repository.CreateAsync(
            new CreateFieldRequest
            {
                FieldLabel = label,
                FieldMergeTag = label.ToLower()
            });

        result.Should().NotBeNull();
        result.Field.Should().NotBeNull();
        result.Field!.FieldLabel.Should().Be(label);

        return result.Field;
    }
}