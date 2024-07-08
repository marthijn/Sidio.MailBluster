using System.Text.Json.Serialization;

namespace Sidio.MailBluster.Responses;

/// <summary>
/// The MailBluster response.
/// </summary>
public record MailBlusterResponse : IMailBlusterResponse
{
    /// <inheritdoc />
    [JsonPropertyName("message")]
    public string? Message { get; init; }
}