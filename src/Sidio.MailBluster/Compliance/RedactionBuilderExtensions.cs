using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Compliance.Redaction;

namespace Sidio.MailBluster.Compliance;

/// <summary>
/// The redaction builder extensions.
/// </summary>
public static class RedactionBuilderExtensions
{
    /// <summary>
    /// Adds the MailBluster compliance.
    /// </summary>
    /// <param name="redactionBuilder">The redaction builder.</param>
    /// <returns>The <see cref="IRedactionBuilder"/>.</returns>
    [ExcludeFromCodeCoverage]
    public static IRedactionBuilder AddMailBlusterCompliance(this IRedactionBuilder redactionBuilder)
    {
        redactionBuilder.SetRedactor<StarRedactor>(MailBlusterDataTaxonomy.SensitiveInformation);
        redactionBuilder.SetRedactor<StarRedactor>(MailBlusterDataTaxonomy.PersonallyIdentifiableInformation);
        redactionBuilder.SetRedactor<EmailRedactor>(MailBlusterDataTaxonomy.EmailAddressInformation);
        return redactionBuilder;
    }
}