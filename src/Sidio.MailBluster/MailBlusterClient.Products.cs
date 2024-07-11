using Flurl.Http;
using Microsoft.Extensions.Logging;
using Sidio.MailBluster.Requests.Products;
using Sidio.MailBluster.Responses.Products;

namespace Sidio.MailBluster;

public sealed partial class MailBlusterClient
{
    /// <inheritdoc />
    public async Task<GetProductsResponse> GetProductsAsync(
        int? pageNo = null,
        int? perPage = null,
        CancellationToken cancellationToken = default)
    {
        if (DebugLogEnabled)
        {
            _logger.LogDebug("Get all products");
        }

        var response = await DefaultClient
            .Request(MailBlusterApiConstants.Products)
            .AppendQueryParam("pageNo", pageNo)
            .AppendQueryParam("perPage", perPage)
            .GetAndHandleErrorAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        return await response.GetJsonAsync<GetProductsResponse>().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<GetProductResponse?> GetProductAsync(string id, CancellationToken cancellationToken = default)
    {
        if (DebugLogEnabled)
        {
            _logger.LogDebug("Get product with id `{ProductId}`", id.Sanitize());
        }

        IFlurlResponse? response = null;
        try
        {
            response = await DefaultClient
                .Request(MailBlusterApiConstants.Products, id)
                .GetAndHandleErrorAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            return await response.GetJsonAsync<GetProductResponse>().ConfigureAwait(false);
        }
        catch (MailBlusterNoContentException)
        {
            if (DebugLogEnabled)
            {
                _logger.LogDebug(
                    "Response status code {StatusCode} for get product with id `{ProductId}`",
                    response?.StatusCode,
                    id.Sanitize());
            }
        }

        return null;
    }

    /// <inheritdoc />
    public async Task<CreateProductResponse> CreateProductAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        if (DebugLogEnabled)
        {
            _logger.LogDebug("Creating product with id `{ProductId}`", request.Id.Sanitize());
        }

        var response = await DefaultClient
            .Request(MailBlusterApiConstants.Products)
            .PostJsonAndHandleErrorAsync(request, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        var result = await response.GetJsonAsync<CreateProductResponse>().ConfigureAwait(false);
        return result;
    }

    /// <inheritdoc />
    public async Task<UpdateProductResponse> UpdateProductAsync(string id, UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        if (DebugLogEnabled)
        {
            _logger.LogDebug("Updating product with id `{ProductId}`", id.Sanitize());
        }

        var response = await DefaultClient
            .Request(MailBlusterApiConstants.Products, id)
            .PutJsonAndHandleErrorAsync(request, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        var result = await response.GetJsonAsync<UpdateProductResponse>().ConfigureAwait(false);
        return result;
    }

    /// <inheritdoc />
    public async Task<DeleteProductResponse> DeleteProductAsync(string id, CancellationToken cancellationToken = default)
    {
        if (DebugLogEnabled)
        {
            _logger.LogDebug("Deleting product with id `{ProductId}`", id.Sanitize());
        }

        IFlurlResponse? response = null;
        try
        {
            response = await DefaultClient
                .Request(MailBlusterApiConstants.Products, id)
                .DeleteAndHandleErrorAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            var result = await response.GetJsonAsync<DeleteProductResponse>().ConfigureAwait(false);
            return result;
        }
        catch (MailBlusterNoContentException ex)
        {
            if (DebugLogEnabled)
            {
                _logger.LogDebug(
                    "Response status code {StatusCode} for delete product with id `{ProductId}`",
                    response?.StatusCode,
                    id.Sanitize());
            }

            return new DeleteProductResponse
            {
                Message = ex.Message
            };
        }
    }
}