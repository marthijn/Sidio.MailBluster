namespace Sidio.MailBluster.Integration.Tests.Context;

[Binding]
public sealed class LeadContext
{
    public string? Email { get; set; }

    public string? LastName { get; set; }
}