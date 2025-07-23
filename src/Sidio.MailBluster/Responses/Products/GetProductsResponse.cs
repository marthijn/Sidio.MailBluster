using System.Text.Json.Serialization;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Responses.Products;

/// <summary>
/// The get products response.
/// </summary>
public sealed class GetProductsResponse
{
    /// <summary>
    /// Gets the products.
    /// </summary>
    [JsonPropertyName("products")]
    public IReadOnlyList<Product> Products { get; init; } = [];

    /// <summary>
    /// Gets the page metadata.
    /// </summary>
    [JsonPropertyName("meta")]
    public required PageMetaData Meta { get; init; }
}