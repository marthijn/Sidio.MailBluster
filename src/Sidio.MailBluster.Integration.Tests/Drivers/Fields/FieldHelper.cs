namespace Sidio.MailBluster.Integration.Tests.Drivers.Fields;

internal static class FieldHelper
{
    public static string CreateValidFieldLabel()
    {
        return $"t{Guid.NewGuid():N}";
    }
}