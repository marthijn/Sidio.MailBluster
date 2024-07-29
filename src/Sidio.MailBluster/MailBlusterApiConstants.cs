namespace Sidio.MailBluster;

internal static class MailBlusterApiConstants
{
    public const string Leads = "leads";
    public const string Fields = "fields";
    public const string Products = "products";

    public static readonly string[] NoContentResponseMessages =
        ["Lead not found", "Field not found", "Product not found"];
}