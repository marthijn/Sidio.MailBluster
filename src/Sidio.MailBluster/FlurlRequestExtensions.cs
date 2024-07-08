using System.Diagnostics.CodeAnalysis;
using System.Net;
using Flurl.Http;
using Sidio.MailBluster.Responses;

namespace Sidio.MailBluster;

internal static class FlurlRequestExtensions
{
    public static async Task<IFlurlResponse> GetAndHandleErrorAsync(
        this IFlurlRequest request,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await request.GetAsync(completionOption, cancellationToken).ConfigureAwait(false);
        }
        catch (FlurlHttpException httpException)
        {
            return await HandleException(httpException).ConfigureAwait(false);
        }
    }

    public static async Task<IFlurlResponse> PostJsonAndHandleErrorAsync(
        this IFlurlRequest request,
        object body,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await request.PostJsonAsync(body, completionOption, cancellationToken).ConfigureAwait(false);
        }
        catch (FlurlHttpException httpException)
        {
            return await HandleException(httpException).ConfigureAwait(false);
        }
    }

    public static async Task<IFlurlResponse> PutJsonAndHandleErrorAsync(
        this IFlurlRequest request,
        object body,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await request.PutJsonAsync(body, completionOption, cancellationToken).ConfigureAwait(false);
        }
        catch (FlurlHttpException httpException)
        {
            return await HandleException(httpException).ConfigureAwait(false);
        }
    }

    public static async Task<IFlurlResponse> DeleteAndHandleErrorAsync(
        this IFlurlRequest request,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await request.DeleteAsync(completionOption, cancellationToken).ConfigureAwait(false);
        }
        catch (FlurlHttpException httpException)
        {
            return await HandleException(httpException).ConfigureAwait(false);
        }
    }

    [DoesNotReturn]
    private static async Task<IFlurlResponse> HandleException(
        FlurlHttpException httpException)
    {
        if (httpException.StatusCode == (int)HttpStatusCode.UnprocessableEntity)
        {
            var entities = await httpException.GetResponseJsonAsync<UnprocessableEntityResponse>()
                .ConfigureAwait(false);
            throw new MailBlusterUnprocessableEntityException(entities, httpException);
        }

        if (httpException.StatusCode == (int) HttpStatusCode.NotFound)
        {
            var notFoundResponse = await httpException.GetResponseJsonAsync<MailBlusterResponse>().ConfigureAwait(false);
            if (notFoundResponse?.Message != null && !notFoundResponse.Message.Equals(
                    "API endpoint does not exist",
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new MailBlusterNoContentException();
            }

            throw new MailBlusterHttpException(null, notFoundResponse?.Message, httpException);
        }

        var errorResponse = await httpException.GetResponseJsonAsync<ErrorResponse>().ConfigureAwait(false);
        throw new MailBlusterHttpException(errorResponse.Code, errorResponse.Message, httpException);
    }
}