using Flurl.Http;
using Sidio.MailBluster.Responses;

namespace Sidio.MailBluster;

/// <summary>
/// The MailBluster HTTP exception.
/// </summary>
public class MailBlusterHttpException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MailBlusterHttpException"/> class.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The message.</param>
    /// <param name="innerException">The inner exception.</param>
    public MailBlusterHttpException(string? code, string? message, Exception? innerException = null)
        : base(message, innerException)
    {
        Code = code;
    }

    /// <summary>
    /// The raw error code.
    /// </summary>
    public string? Code { get; }

    /// <summary>
    /// The error code.
    /// </summary>
    public ErrorCode ErrorCode =>
        Enum.TryParse<ErrorCode>(Code, true, out var errorCode) ? errorCode : ErrorCode.Unknown;

    /// <summary>
    /// The HTTP status code.
    /// </summary>
    public int? StatusCode => (InnerException as FlurlHttpException)?.Call?.Response?.StatusCode;
}