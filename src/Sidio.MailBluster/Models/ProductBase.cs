using System.Text.Json.Serialization;

namespace Sidio.MailBluster.Models;

/// <summary>
/// The product base.
/// </summary>
public abstract record ProductBase
{
    /// <summary>
    /// Gets the name of the product.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }
}