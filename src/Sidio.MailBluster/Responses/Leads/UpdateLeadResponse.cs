using System.Text.Json.Serialization;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Responses.Leads;

/// <summary>
/// The update lead response.
/// </summary>
public sealed class UpdateLeadResponse : MailBlusterResponse
{
    /// <summary>
    /// Gets the lead.
    /// </summary>
    [JsonPropertyName("lead")]
    public Lead? Lead { get; set; }
}