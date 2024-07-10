using System.Text.Json.Serialization;

namespace Sidio.MailBluster.Responses.Products;

/// <summary>
/// The delete product response.
/// </summary>
public sealed record DeleteProductResponse : MailBlusterDeleteResponse
{
    /// <summary>
    /// Gets the (unique) id.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <inheritdoc />
    public override bool Success => !string.IsNullOrWhiteSpace(Id);
}