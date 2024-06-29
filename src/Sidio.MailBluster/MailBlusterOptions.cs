namespace Sidio.MailBluster;

/// <summary>
/// The MailBluster options.
/// </summary>
public sealed record MailBlusterOptions
{
    /// <summary>
    /// The config section name.
    /// </summary>
    public const string SectionName = "MailBluster";

    /// <summary>
    /// Gets the API base url.
    /// </summary>
    public string Url { get; init; } = "https://api.mailbluster.com/api/";

    /// <summary>
    /// Gets the API key.
    /// </summary>
    public required string ApiKey { get; init; }
}