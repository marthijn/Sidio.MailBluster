using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Compliance.Classification;

namespace Sidio.MailBluster.Compliance;

/// <summary>
/// The Personally Identifiable Information (PII) data classification attribute.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class PersonallyIdentifiableInformationAttribute : DataClassificationAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PersonallyIdentifiableInformationAttribute"/> class.
    /// </summary>
    public PersonallyIdentifiableInformationAttribute() : base(MailBlusterDataTaxonomy.PersonallyIdentifiableInformation)
    {
    }
}