using System.Text.Json.Serialization;

namespace Sidio.MailBluster.Models;

public record Lead : LeadBase
{
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? Id { get; init; }

    [JsonPropertyName("fullName")]
    public string? FullName { get; init; }

    [JsonPropertyName("optInStatus")]
    public string? OptInStatus { get; init; }

    [JsonPropertyName("createdAt")]
    public string? CreatedAt { get; init; }

    [JsonPropertyName("updatedAt")]
    public string? UpdatedAt { get; init; }
}