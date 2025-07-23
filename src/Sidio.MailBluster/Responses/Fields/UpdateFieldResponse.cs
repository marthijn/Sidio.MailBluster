using System.Text.Json.Serialization;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Responses.Fields;

/// <summary>
/// Gets the update field response.
/// </summary>
public sealed class UpdateFieldResponse : MailBlusterResponse
{
    /// <summary>
    /// Gets the field.
    /// </summary>
    [JsonPropertyName("field")]
    public Field? Field { get; set; }
}