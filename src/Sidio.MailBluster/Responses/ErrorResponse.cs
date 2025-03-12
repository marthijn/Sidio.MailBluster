using System.Text.Json.Serialization;

namespace Sidio.MailBluster.Responses;

/// <summary>
/// The error response.
/// </summary>
public sealed record ErrorResponse : MailBlusterResponse
{
    /// <summary>
    /// Gets the error code.
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; init; }
}