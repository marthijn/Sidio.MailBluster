using System.Text.Json.Serialization;

namespace Sidio.MailBluster.Responses.Leads;

public sealed record DeleteLeadResponse : MailBlusterResponse
{
    [JsonPropertyName("leadHash")]
    public string? LeadHash { get; set; }
}