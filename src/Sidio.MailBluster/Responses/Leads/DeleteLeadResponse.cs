using System.Text.Json.Serialization;

namespace Sidio.MailBluster.Responses.Leads;

/// <summary>
/// The delete lead response.
/// </summary>
public sealed record DeleteLeadResponse : MailBlusterDeleteResponse
{
    /// <summary>
    /// Gets the lead hash.
    /// </summary>
    [JsonPropertyName("leadHash")]
    public string? LeadHash { get; init; }

    /// <inheritdoc />
    public override bool Success => !string.IsNullOrWhiteSpace(LeadHash);
}