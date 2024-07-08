using Sidio.MailBluster.Integration.Tests.Repositories;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Integration.Tests.Drivers.Fields;

[Binding]
public sealed class FieldExistsDriver
{
    private readonly FieldRepository _respository;

    public FieldExistsDriver(FieldRepository respository)
    {
        _respository = respository;
    }

    public async Task<Field> FieldShouldExistAsync(string label)
    {
        var results = await _respository.GetListAsync();
        results.Should().NotBeNull();
        var result = results.SingleOrDefault(x => x.FieldLabel == label);
        result.Should().NotBeNull();
        return result!;
    }

    public async Task FieldShouldNotExistAsync(string label)
    {
        var result = await _respository.GetListAsync();
        result.Should().NotBeNull();
        result.Any(x => x.FieldLabel == label).Should().BeFalse();
    }
}