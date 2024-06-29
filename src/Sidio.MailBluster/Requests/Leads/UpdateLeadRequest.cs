using System.Text.Json.Serialization;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Requests.Leads;

/// <summary>
/// Updates a lead request.
/// </summary>
public sealed record UpdateLeadRequest : LeadBase
{
    /// <summary>
    /// Gets the tags to attach.
    /// </summary>
    [JsonPropertyName("addTags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? AddTags { get; init; }

    /// <summary>
    /// Gets the tags to remove.
    /// </summary>
    [JsonPropertyName("removeTags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? RemoveTags { get; init; }
}