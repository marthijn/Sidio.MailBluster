using System.Text.Json.Serialization;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Responses.Leads;

public sealed record UpdateLeadResponse : MailBlusterResponse
{
    [JsonPropertyName("lead")]
    public Lead? Lead { get; set; }
}