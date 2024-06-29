using System.Text.Json.Serialization;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Responses.Leads;

/// <summary>
/// The create lead response.
/// </summary>
public sealed record CreateLeadResponse : MailBlusterResponse
{
    /// <summary>
    /// Gets the lead.
    /// </summary>
    [JsonPropertyName("lead")]
    public Lead? Lead { get; set; }
}