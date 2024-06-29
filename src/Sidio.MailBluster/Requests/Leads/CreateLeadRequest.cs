using System.Text.Json.Serialization;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Requests.Leads;

/// <summary>
/// The create lead request.
/// </summary>
public sealed record CreateLeadRequest : LeadBase
{
    /// <summary>
    /// Gets the tags.
    /// </summary>
    [JsonPropertyName("tags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Tags { get; init; }

    /// <summary>
    /// Gets a value indicating whether the double opt-in is enabled.
    /// </summary>
    [JsonPropertyName("doubleOptIn")]
    public bool DoubleOptIn { get; init; }

    /// <summary>
    /// Gets a value indicating whether to override existing leads.
    /// </summary>
    [JsonPropertyName("overrideExisting")]
    public bool OverrideExisting { get; init; }
}