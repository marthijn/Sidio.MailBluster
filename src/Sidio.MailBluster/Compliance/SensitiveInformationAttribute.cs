using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Compliance.Classification;

namespace Sidio.MailBluster.Compliance;

/// <summary>
/// The Sensitive Information data classification attribute.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class SensitiveInformationAttribute : DataClassificationAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SensitiveInformationAttribute"/> class.
    /// </summary>
    public SensitiveInformationAttribute() : base(MailBlusterDataTaxonomy.SensitiveInformation)
    {
    }
}