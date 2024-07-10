using System.Text.Json.Serialization;

namespace Sidio.MailBluster.Models;

/// <summary>
/// The product.
/// </summary>
public record Product : ProductBase
{
    /// <summary>
    /// Gets the (unique) id.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

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