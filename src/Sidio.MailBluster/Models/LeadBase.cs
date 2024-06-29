using System.Text.Json.Serialization;

namespace Sidio.MailBluster.Models;

public abstract record LeadBase
{
    [JsonPropertyName("firstName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FirstName { get; init; }

    [JsonPropertyName("lastName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LastName { get; init; }

    [JsonPropertyName("email")]
    public required string Email { get; init; }

    [JsonPropertyName("subscribed")]
    public required bool Subscribed { get; init; }

    [JsonPropertyName("fields")]
    public Fields? Fields { get; init; }

    [JsonPropertyName("timezone")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Timezone { get; init; }

    [JsonPropertyName("ipAddress")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IpAddress { get; init; }

    [JsonPropertyName("tags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Tags { get; init; }
}