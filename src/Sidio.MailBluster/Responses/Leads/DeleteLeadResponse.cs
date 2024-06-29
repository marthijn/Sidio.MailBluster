using System.Text.Json.Serialization;

namespace Sidio.MailBluster.Responses.Leads;

/// <summary>
/// The delete lead response.
/// </summary>
public sealed record DeleteLeadResponse : MailBlusterResponse
{
    /// <summary>
    /// Gets the lead hash.
    /// </summary>
    [JsonPropertyName("leadHash")]
    public string? LeadHash { get; set; }
}