using System.Text.Json.Serialization;

namespace Sidio.MailBluster.Responses.Fields;

/// <summary>
/// The delete field response.
/// </summary>
public sealed record DeleteFieldResponse : MailBlusterResponse
{
    /// <summary>
    /// Gets the id of the field that was deleted.
    /// </summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }
}