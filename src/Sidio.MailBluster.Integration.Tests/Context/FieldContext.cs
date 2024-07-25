using Sidio.MailBluster.Models;

namespace Sidio.MailBluster.Integration.Tests.Context;

[Binding]
public sealed class FieldContext
{
    public string? Label { get; set; }

    public Field? Field { get; set; }
}