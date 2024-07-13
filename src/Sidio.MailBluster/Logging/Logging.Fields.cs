using Microsoft.Extensions.Logging;
using Sidio.MailBluster.Requests.Fields;
using Sidio.MailBluster.Responses.Fields;

namespace Sidio.MailBluster.Logging;

internal static partial class Logging
{
    [LoggerMessage(LogLevel.Debug, "Getting all fields")]
    public static partial void LogGetFieldsRequest(
        this ILogger logger);

    [LoggerMessage(LogLevel.Debug, "Fields received")]
    public static partial void LogGetFieldsResponse(
        this ILogger logger,
        [LogProperties] GetFieldsResponse response);

    [LoggerMessage(LogLevel.Debug, "Creating field with label {Label}")]
    public static partial void LogCreateFieldRequest(
        this ILogger logger,
        string label,
        [LogProperties] CreateFieldRequest request);

    [LoggerMessage(LogLevel.Debug, "Field with label {Label} created")]
    public static partial void LogCreateFieldResponse(
        this ILogger logger,
        string label,
        [LogProperties] CreateFieldResponse response);

    [LoggerMessage(LogLevel.Debug, "Updating field with id {FieldId}")]
    public static partial void LogUpdateFieldRequest(
        this ILogger logger,
        long fieldId,
        [LogProperties] UpdateFieldRequest request);

    [LoggerMessage(LogLevel.Debug, "Field with id {FieldId} updated")]
    public static partial void LogUpdateFieldResponse(
        this ILogger logger,
        long fieldId,
        [LogProperties] UpdateFieldResponse response);

    [LoggerMessage(LogLevel.Debug, "Deleting field with id {FieldId}")]
    public static partial void LogDeleteFieldRequest(
        this ILogger logger,
        long fieldId);

    [LoggerMessage(LogLevel.Debug, "Field with id {FieldId} deleted")]
    public static partial void LogDeleteFieldResponse(
        this ILogger logger,
        long fieldId,
        [LogProperties] DeleteFieldResponse response);

    [LoggerMessage(LogLevel.Debug, "Field with id {FieldId} is not found")]
    public static partial void LogFieldNotFound(this ILogger logger, long fieldId);
}