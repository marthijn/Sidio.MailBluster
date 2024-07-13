using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Compliance.Classification;

namespace Sidio.MailBluster.Compliance;

/// <summary>
/// The email information data classification attribute.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class EmailAddressInformationAttribute : DataClassificationAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAddressInformationAttribute"/> class.
    /// </summary>
    public EmailAddressInformationAttribute() : base(MailBlusterDataTaxonomy.EmailAddressInformation)
    {
    }
}