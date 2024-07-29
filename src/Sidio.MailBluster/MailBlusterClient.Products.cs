using Flurl.Http;
using Sidio.MailBluster.Logging;
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
        _logger.LogGetProductsRequest(pageNo, perPage);
        var response = await DefaultClient
            .Request(MailBlusterApiConstants.Products)
            .AppendQueryParam("pageNo", pageNo)
            .AppendQueryParam("perPage", perPage)
            .GetAndHandleErrorAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        var result = await response.GetJsonAsync<GetProductsResponse>().ConfigureAwait(false);
        _logger.LogGetProductsResponse(result);
        return result;
    }

    /// <inheritdoc />
    public async Task<GetProductResponse?> GetProductAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogGetProductRequest(id);
        try
        {
            var response = await DefaultClient
                .Request(MailBlusterApiConstants.Products, id)
                .GetAndHandleErrorAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            var result = await response.GetJsonAsync<GetProductResponse>().ConfigureAwait(false);
            _logger.LogGetProductResponse(id, result);
            return result;
        }
        catch (MailBlusterNoContentException)
        {
            _logger.LogProductNotFound(id);
        }

        return null;
    }

    /// <inheritdoc />
    public async Task<CreateProductResponse> CreateProductAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogCreateProductRequest(request.Name, request);
        var response = await DefaultClient
            .Request(MailBlusterApiConstants.Products)
            .PostJsonAndHandleErrorAsync(request, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        var result = await response.GetJsonAsync<CreateProductResponse>().ConfigureAwait(false);
        _logger.LogCreateProductResponse(request.Name, result);
        return result;
    }

    /// <inheritdoc />
    public async Task<UpdateProductResponse> UpdateProductAsync(string id, UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogUpdateProductRequest(id, request);
        var response = await DefaultClient
            .Request(MailBlusterApiConstants.Products, id)
            .PutJsonAndHandleErrorAsync(request, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        var result = await response.GetJsonAsync<UpdateProductResponse>().ConfigureAwait(false);
        _logger.LogUpdateProductResponse(id, result);
        return result;
    }

    /// <inheritdoc />
    public async Task<DeleteProductResponse> DeleteProductAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogDeleteProductRequest(id);
        try
        {
            var response = await DefaultClient
                .Request(MailBlusterApiConstants.Products, id)
                .DeleteAndHandleErrorAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            var result = await response.GetJsonAsync<DeleteProductResponse>().ConfigureAwait(false);
            _logger.LogDeleteProductResponse(id, result);
            return result;
        }
        catch (MailBlusterNoContentException ex)
        {
            _logger.LogProductNotFound(id);

            return new DeleteProductResponse
            {
                Message = ex.Message
            };
        }
    }
}