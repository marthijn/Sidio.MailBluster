namespace Sidio.MailBluster.Examples.MvcWebApplication.Models;

public sealed record SetApiKeyModel
{
    public required string ApiKey { get; init; }
}