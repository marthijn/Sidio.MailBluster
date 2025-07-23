using System.Text.Json.Serialization;

namespace Sidio.MailBluster.Responses.Fields;

/// <summary>
/// The delete field response.
/// </summary>
public sealed class DeleteFieldResponse : MailBlusterDeleteResponse
{
    /// <summary>
    /// Gets the id of the field that was deleted.
    /// </summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <inheritdoc />
    public override bool Success => Id != null;
}