using Sidio.MailBluster.Integration.Tests.Repositories;

namespace Sidio.MailBluster.Integration.Tests.Drivers.Fields;

[Binding]
public sealed class DeleteFieldDriver
{
    private readonly FieldRepository _repository;

    public DeleteFieldDriver(FieldRepository repository)
    {
        _repository = repository;
    }

    public async Task DeleteFieldAsync(long id)
    {
        var result = await _repository.DeleteAsync(id);
        result.Should().NotBeNull();
    }
}