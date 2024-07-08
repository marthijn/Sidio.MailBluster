using System.Text.Json.Serialization;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Responses.Fields;

/// <summary>
/// The get fields response.
/// </summary>
public sealed record GetFieldsResponse
{
    /// <summary>
    /// Gets the fields.
    /// </summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<Field> Fields { get; init; } = [];
}