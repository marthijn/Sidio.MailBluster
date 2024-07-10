using System.Text.Json.Serialization;

namespace Sidio.MailBluster.Models;

/// <summary>
/// The field base class.
/// </summary>
public record FieldBase
{
    /// <summary>
    /// Gets the field label.
    /// </summary>
    [JsonPropertyName("fieldLabel")]
    public required string FieldLabel { get; init; }

    /// <summary>
    /// Gets the field merge tag.
    /// The field merge tag can contain letter, number and underscore only.
    /// The field merge tag cannot start with a number.
    /// </summary>
    [JsonPropertyName("fieldMergeTag")]
    public required string FieldMergeTag { get; init; }
}