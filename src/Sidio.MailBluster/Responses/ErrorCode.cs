namespace Sidio.MailBluster.Responses;

/// <summary>
/// The error code enum.
/// </summary>
public enum ErrorCode
{
    /// <summary>
    /// Indicates that the request is not authenticated.
    /// </summary>
    Unauthenticated,

    /// <summary>
    /// Indicates that the requested feature is locked.
    /// </summary>
    FeatureLocked,

    /// <summary>
    /// Unknown error code.
    /// </summary>
    Unknown
}