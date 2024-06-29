using Sidio.MailBluster.Responses.Leads;

namespace Sidio.MailBluster.Examples.MvcWebApplication.Models;

public sealed record GetLeadModel
{
    public required string EmailAddress { get; init; }

    public GetLeadResponse? Lead { get; set; }
}