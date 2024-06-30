namespace Sidio.MailBluster.Integration.Tests.Drivers;

[Binding]
public sealed class LeadExistsDriver
{
    private readonly IMailBlusterClient _client;

    public LeadExistsDriver(IMailBlusterClient client)
    {
        _client = client;
    }

    public async Task LeadShouldNotExistAsync(string email)
    {
        var result = await _client.GetLeadAsync(email);
        result.Should().BeNull();
    }

    public async Task LeadShouldExistAsync(string email)
    {
        var result = await _client.GetLeadAsync(email);
        result.Should().NotBeNull();
        result!.Email.Should().Be(email);
    }
}