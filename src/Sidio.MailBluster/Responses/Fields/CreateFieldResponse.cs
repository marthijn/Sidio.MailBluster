using System.Text.Json.Serialization;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Responses.Fields;

/// <summary>
/// The create field response.
/// </summary>
public sealed record CreateFieldResponse : MailBlusterResponse
{
    /// <summary>
    /// Gets the field.
    /// </summary>
    [JsonPropertyName("field")]
    public Field? Field { get; set; }
}