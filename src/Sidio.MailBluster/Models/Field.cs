using System.Text.Json.Serialization;

namespace Sidio.MailBluster.Models;

/// <summary>
/// The field.
/// </summary>
public sealed record Field : FieldBase
{
    /// <summary>
    /// Gets the id.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? Id { get; init; }

    /// <summary>
    /// Gets the created date.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public string? CreatedAt { get; init; }

    /// <summary>
    /// Gets the updated date.
    /// </summary>
    [JsonPropertyName("updatedAt")]
    public string? UpdatedAt { get; init; }
}