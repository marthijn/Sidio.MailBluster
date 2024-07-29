using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Compliance.Classification;

namespace Sidio.MailBluster.Compliance;

/// <summary>
/// The MailBluster data taxonomy.
/// </summary>
[ExcludeFromCodeCoverage]
public static class MailBlusterDataTaxonomy
{
    private const string TaxonomyName = nameof(MailBlusterDataTaxonomy);

    /// <summary>
    /// Gets the personally identifiable information data classification.
    /// </summary>
    public static DataClassification PersonallyIdentifiableInformation { get; } = new (
        TaxonomyName,
        nameof(PersonallyIdentifiableInformation));

    /// <summary>
    /// Gets the sensitive information data classification.
    /// </summary>
    public static DataClassification SensitiveInformation { get; } = new (
        TaxonomyName,
        nameof(SensitiveInformation));

    /// <summary>
    /// Gets the email address information data classification.
    /// </summary>
    public static DataClassification EmailAddressInformation { get; } =
        new(TaxonomyName, nameof(EmailAddressInformation));
}