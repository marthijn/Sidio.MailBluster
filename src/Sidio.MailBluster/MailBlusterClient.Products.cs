using RestSharp;
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

        var restRequest = new RestRequest(MailBlusterApiConstants.Products);
        restRequest.AddQueryParameter("pageNo", pageNo?.ToString() ?? string.Empty);
        restRequest.AddQueryParameter("perPage", perPage?.ToString() ?? string.Empty);

        var response = await _restClient.ExecuteAsync(restRequest, cancellationToken).ConfigureAwait(false);
        var result = HandleResponse<GetProductsResponse>(response);

        _logger.LogGetProductsResponse(result);
        return result;
    }

    /// <inheritdoc />
    public async Task<GetProductResponse?> GetProductAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogGetProductRequest(id);
        try
        {
            var restRequest = new RestRequest(MailBlusterApiConstants.ProductsById);
            restRequest.AddUrlSegment(MailBlusterApiConstants.ProductId, id);

            var response = await _restClient.ExecuteAsync(restRequest, cancellationToken).ConfigureAwait(false);
            var result = HandleResponse<GetProductResponse>(response);

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

        var restRequest = new RestRequest(MailBlusterApiConstants.Products, Method.Post);
        restRequest.AddJsonBody(request);

        var response = await _restClient.ExecuteAsync(restRequest, cancellationToken).ConfigureAwait(false);
        var result = HandleResponse<CreateProductResponse>(response);

        _logger.LogCreateProductResponse(request.Name, result);
        return result;
    }

    /// <inheritdoc />
    public async Task<UpdateProductResponse> UpdateProductAsync(string id, UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogUpdateProductRequest(id, request);

        var restRequest = new RestRequest(MailBlusterApiConstants.ProductsById, Method.Put);
        restRequest.AddUrlSegment(MailBlusterApiConstants.ProductId, id);
        restRequest.AddJsonBody(request);

        var response = await _restClient.ExecuteAsync(restRequest, cancellationToken).ConfigureAwait(false);
        var result = HandleResponse<UpdateProductResponse>(response);

        _logger.LogUpdateProductResponse(id, result);
        return result;
    }

    /// <inheritdoc />
    public async Task<DeleteProductResponse> DeleteProductAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogDeleteProductRequest(id);
        try
        {
            var restRequest = new RestRequest(MailBlusterApiConstants.ProductsById, Method.Delete);
            restRequest.AddUrlSegment(MailBlusterApiConstants.ProductId, id);

            var response = await _restClient.ExecuteAsync(restRequest, cancellationToken).ConfigureAwait(false);
            var result = HandleResponse<DeleteProductResponse>(response);

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