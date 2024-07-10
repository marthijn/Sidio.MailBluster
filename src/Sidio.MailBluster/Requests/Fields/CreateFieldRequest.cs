using System.Text.Json.Serialization;

namespace Sidio.MailBluster.Requests.Fields;

/// <summary>
/// The create field request.
/// </summary>
public sealed record CreateFieldRequest
{
    /// <summary>
    /// Gets the label of the field.
    /// </summary>
    [JsonPropertyName("fieldLabel")]
    public required string FieldLabel { get; init; }

    /// <summary>
    /// Gets the merge tag of the field.
    /// The field merge tag can contain letter, number and underscore only.
    /// The field merge tag cannot start with a number.
    /// </summary>
    [JsonPropertyName("fieldMergeTag")]
    public required string FieldMergeTag { get; init; }
}