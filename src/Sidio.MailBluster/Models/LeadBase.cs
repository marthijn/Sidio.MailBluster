using System.Text.Json.Serialization;

namespace Sidio.MailBluster.Models;

/// <summary>
/// The lead base properties.
/// </summary>
public abstract record LeadBase
{
    /// <summary>
    /// Gets the first name.
    /// </summary>
    [JsonPropertyName("firstName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FirstName { get; init; }

    /// <summary>
    /// Gets the last name.
    /// </summary>
    [JsonPropertyName("lastName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LastName { get; init; }

    /// <summary>
    /// Gets the email address.
    /// </summary>
    [JsonPropertyName("email")]
    public required string Email { get; init; }

    /// <summary>
    /// Gets a value indicating whether the lead is subscribed.
    /// </summary>
    [JsonPropertyName("subscribed")]
    public required bool Subscribed { get; init; }

    /// <summary>
    /// Gets the fields.
    /// </summary>
    [JsonPropertyName("fields")]
    public Fields? Fields { get; init; }

    /// <summary>
    /// Gets the timezone.
    /// </summary>
    [JsonPropertyName("timezone")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Timezone { get; init; }

    /// <summary>
    /// Gets the IP address.
    /// </summary>
    [JsonPropertyName("ipAddress")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IpAddress { get; init; }

    /// <summary>
    /// Gets the tags.
    /// </summary>
    [JsonPropertyName("tags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Tags { get; init; }
}