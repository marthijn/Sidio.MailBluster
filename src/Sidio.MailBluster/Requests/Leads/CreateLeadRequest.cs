using System.Text.Json.Serialization;
using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Requests.Leads;

public sealed record CreateLeadRequest : LeadBase
{
    [JsonPropertyName("doubleOptIn")]
    public bool DoubleOptIn { get; init; }

    [JsonPropertyName("overrideExisting")]
    public bool OverrideExisting { get; init; }
}