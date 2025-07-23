namespace Sidio.MailBluster.Responses;

/// <summary>
/// The abstract response from a delete request.
/// </summary>
public abstract class MailBlusterDeleteResponse : MailBlusterResponse
{
    /// <summary>
    /// Indicates if the delete request was successful.
    /// </summary>
    public abstract bool Success { get; }
}