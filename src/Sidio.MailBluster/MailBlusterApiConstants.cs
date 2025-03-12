namespace Sidio.MailBluster;

internal static class MailBlusterApiConstants
{
    public const string Leads = "leads";
    public const string LeadHash = "lead_hash";
    public const string LeadsByHash = $"leads/{{{LeadHash}}}";

    public const string Fields = "fields";
    public const string FieldId = "field_id";
    public const string FieldsById = $"fields/{{{FieldId}}}";

    public const string Products = "products";
    public const string ProductId = "product_id";
    public const string ProductsById = $"products/{{{ProductId}}}";

    public static readonly string[] NoContentResponseMessages =
        ["Lead not found", "Field not found", "Product not found"];
}