namespace Sidio.MailBluster;

/// <summary>
/// The MailBluster options.
/// </summary>
public sealed class MailBlusterOptions
{
    /// <summary>
    /// The config section name.
    /// </summary>
    public const string SectionName = "MailBluster";

    /// <summary>
    /// Gets or sets the API base url.
    /// </summary>
    public string Url { get; set; } = "https://api.mailbluster.com/api/";

    /// <summary>
    /// Gets or sets the API key.
    /// </summary>
    public required string ApiKey { get; set; }
}