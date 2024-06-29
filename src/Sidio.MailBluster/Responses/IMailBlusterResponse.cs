namespace Sidio.MailBluster.Responses;

/// <summary>
/// The MailBluster response interface.
/// </summary>
public interface IMailBlusterResponse
{
    /// <summary>
    /// Gets the message.
    /// </summary>
    string? Message { get; init; }
}