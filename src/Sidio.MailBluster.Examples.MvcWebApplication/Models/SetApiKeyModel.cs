using System.Diagnostics.CodeAnalysis;

namespace Sidio.MailBluster.Examples.MvcWebApplication.Models;

[ExcludeFromCodeCoverage]
public sealed record SetApiKeyModel
{
    public required string ApiKey { get; init; }
}