using System.Text.Json.Serialization;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Responses.Products;

/// <summary>
/// The update product response.
/// </summary>
public sealed record UpdateProductResponse : MailBlusterResponse
{
    /// <summary>
    /// Gets the product.
    /// </summary>
    [JsonPropertyName("product")]
    public Product? Product { get; init; }
}