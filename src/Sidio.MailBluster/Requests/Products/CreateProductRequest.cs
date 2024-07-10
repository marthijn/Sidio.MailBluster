using System.Text.Json.Serialization;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Requests.Products;

/// <summary>
/// The create product request.
/// </summary>
public sealed record CreateProductRequest : ProductBase
{
    /// <summary>
    /// Gets the unique id.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }
}