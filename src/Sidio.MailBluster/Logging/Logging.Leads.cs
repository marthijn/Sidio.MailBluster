using Microsoft.Extensions.Logging;
using Sidio.MailBluster.Compliance;
using Sidio.MailBluster.Requests.Leads;
using Sidio.MailBluster.Responses.Leads;

namespace Sidio.MailBluster.Logging;

internal static partial class Logging
{
    [LoggerMessage(LogLevel.Debug, "Lead with email {Email} received")]
    public static partial void LogGetLeadResponse(
        this ILogger logger,
        [EmailAddressInformation] string email,
        [LogProperties] GetLeadResponse getLeadResponse);

    [LoggerMessage(LogLevel.Debug, "Lead with email {Email} is not found")]
    public static partial void LogLeadNotFound(this ILogger logger, [EmailAddressInformation] string email);

    [LoggerMessage(LogLevel.Debug, "Getting lead with email {Email}")]
    public static partial void LogGetLeadRequest(this ILogger logger, [EmailAddressInformation] string email);

    [LoggerMessage(LogLevel.Debug, "Creating lead with email {Email}")]
    public static partial void LogCreateLeadRequest(
        this ILogger logger,
        [EmailAddressInformation] string email,
        [LogProperties] CreateLeadRequest request);

    [LoggerMessage(LogLevel.Debug, "Lead with email {Email} created")]
    public static partial void LogCreateLeadResponse(
        this ILogger logger,
        [EmailAddressInformation] string email,
        [LogProperties] CreateLeadResponse response);

    [LoggerMessage(LogLevel.Debug, "Updating lead with email {Email}")]
    public static partial void LogUpdateLeadRequest(
        this ILogger logger,
        [EmailAddressInformation] string email,
        [LogProperties] UpdateLeadRequest request);

    [LoggerMessage(LogLevel.Debug, "Lead with email {Email} updated")]
    public static partial void LogUpdateLeadResponse(
        this ILogger logger,
        [EmailAddressInformation] string email,
        [LogProperties] UpdateLeadResponse response);

    [LoggerMessage(LogLevel.Debug, "Deleting lead with email {Email}")]
    public static partial void LogDeleteLeadRequest(
        this ILogger logger,
        [EmailAddressInformation] string email);

    [LoggerMessage(LogLevel.Debug, "Lead with email {Email} deleted")]
    public static partial void LogDeleteLeadResponse(
        this ILogger logger,
        [EmailAddressInformation] string email,
        [LogProperties] DeleteLeadResponse response);
}