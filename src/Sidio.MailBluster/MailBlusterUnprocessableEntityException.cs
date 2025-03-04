using Sidio.MailBluster.Responses;

namespace Sidio.MailBluster;

/// <summary>
/// Thrown when the entity cannot be processed by MailBluster.
/// </summary>
public sealed class MailBlusterUnprocessableEntityException : MailBlusterApiException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MailBlusterUnprocessableEntityException"/> class.
    /// </summary>
    /// <param name="response">The response.</param>
    /// <param name="innerException">The inner exception.</param>
    public MailBlusterUnprocessableEntityException(UnprocessableEntityResponse response, Exception? innerException = null)
        : base("Unprocessable entity", innerException)
    {
        UnprocessableEntityResponse = response;
    }

    /// <summary>
    /// Gets the unprocessable entities.
    /// </summary>
    public UnprocessableEntityResponse UnprocessableEntityResponse { get; }
}