namespace Sidio.MailBluster.Responses;

/// <summary>
/// The error response.
/// </summary>
public sealed record ErrorResponse : MailBlusterResponse
{
    /// <summary>
    /// Gets the error code.
    /// </summary>
    public string? Code { get; init; }
}