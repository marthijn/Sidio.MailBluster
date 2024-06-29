namespace Sidio.MailBluster;

public sealed record MailBlusterOptions
{
    public const string SectionName = "MailBluster";

    public string Url { get; init; } = "https://api.mailbluster.com/api/";

    public required string ApiKey { get; init; }
}