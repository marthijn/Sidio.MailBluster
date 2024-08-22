using System.Diagnostics.CodeAnalysis;

namespace Sidio.MailBluster.Examples.MvcWebApplication.Models;

[ExcludeFromCodeCoverage]
public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}