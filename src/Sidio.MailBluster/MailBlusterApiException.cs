namespace Sidio.MailBluster;

/// <summary>
/// The MailBluster API exception.
/// </summary>
public class MailBlusterApiException : Exception
{
    public MailBlusterApiException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}