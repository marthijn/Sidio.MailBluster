using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Integration.Tests.Context;

[Binding]
public sealed class ProductContext
{
    public string? Id { get; set; }

    public Product? Product { get; set; }

    public string? Name { get; set; }
}