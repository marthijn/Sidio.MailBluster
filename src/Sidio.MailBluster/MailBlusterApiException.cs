namespace Sidio.MailBluster;

/// <summary>
/// The MailBluster API exception.
/// </summary>
[Serializable]
public class MailBlusterApiException : Exception
{
    /// <summary>
    /// The MailBluster API exception.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="innerException">The inner exception.</param>
    public MailBlusterApiException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}