using System.Net;
using Sidio.MailBluster.Responses;

namespace Sidio.MailBluster;

/// <summary>
/// The MailBluster HTTP exception.
/// </summary>
[Serializable]
public class MailBlusterHttpException : MailBlusterApiException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MailBlusterHttpException"/> class.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The message.</param>
    /// <param name="content">The reseponse content.</param>
    /// <param name="innerException">The inner exception.</param>
    /// <param name="httpStatusCode">The HTTP status code.</param>
    public MailBlusterHttpException(
        string? code,
        string? message,
        HttpStatusCode httpStatusCode,
        string? content = null,
        Exception? innerException = null)
        : base(message, innerException)
    {
        Code = code;
        HttpStatusCode = httpStatusCode;
        Content = content;
    }

    /// <summary>
    /// The raw error code.
    /// </summary>
    public string? Code { get; }

    /// <summary>
    /// The HTTP status code.
    /// </summary>
    public HttpStatusCode HttpStatusCode { get; }

    /// <summary>
    /// Gets the raw response content.
    /// </summary>
    public string? Content { get; }

    /// <summary>
    /// The error code.
    /// </summary>
    public ErrorCode ErrorCode =>
        Enum.TryParse<ErrorCode>(Code, true, out var errorCode) ? errorCode : ErrorCode.Unknown;
}