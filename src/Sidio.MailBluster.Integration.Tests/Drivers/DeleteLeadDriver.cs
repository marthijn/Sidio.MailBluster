namespace Sidio.MailBluster.Integration.Tests.Drivers;

[Binding]
public sealed class DeleteLeadDriver
{
    private readonly IMailBlusterClient _client;

    public DeleteLeadDriver(IMailBlusterClient client)
    {
        _client = client;
    }

    public async Task DeleteLeadAsync(string email)
    {
        await _client.DeleteLeadAsync(email);
    }
}