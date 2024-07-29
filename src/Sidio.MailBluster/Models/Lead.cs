using System.Text.Json.Serialization;
using Sidio.MailBluster.Compliance;

namespace Sidio.MailBluster.Models;

/// <summary>
/// The lead.
/// </summary>
public record Lead : LeadBase
{
    /// <summary>
    /// Gets the id.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? Id { get; init; }

    /// <summary>
    /// Gets the full name.
    /// </summary>
    [JsonPropertyName("fullName")]
    [PersonallyIdentifiableInformation]
    public string? FullName { get; init; }

    /// <summary>
    /// Gets the opt-in status.
    /// </summary>
    [JsonPropertyName("optInStatus")]
    public string? OptInStatus { get; init; }

    /// <summary>
    /// Gets the tags.
    /// </summary>
    [JsonPropertyName("tags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Tags { get; init; }

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