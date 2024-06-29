using System.Text.Json.Serialization;

namespace Sidio.MailBluster.Responses;

/// <summary>
/// The MailBluster response.
/// </summary>
public abstract record MailBlusterResponse : IMailBlusterResponse
{
    /// <inheritdoc />
    [JsonPropertyName("message")]
    public string? Message { get; init; }
}