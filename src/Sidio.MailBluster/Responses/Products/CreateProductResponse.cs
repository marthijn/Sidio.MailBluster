using System.Text.Json.Serialization;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Responses.Products;

/// <summary>
/// The create product response.
/// </summary>
public sealed class CreateProductResponse : MailBlusterResponse
{
    /// <summary>
    /// Gets the product.
    /// </summary>
    [JsonPropertyName("product")]
    public Product? Product { get; init; }
}