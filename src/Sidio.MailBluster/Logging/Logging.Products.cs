using Microsoft.Extensions.Logging;
using Sidio.MailBluster.Requests.Products;
using Sidio.MailBluster.Responses.Products;

namespace Sidio.MailBluster.Logging;

internal static partial class Logging
{
    [LoggerMessage(LogLevel.Debug, "Getting all products on page {PageNo} with {PerPage} products per page")]
    public static partial void LogGetProductsRequest(
        this ILogger logger, int? pageNo, int? perPage);

    [LoggerMessage(LogLevel.Debug, "Products received")]
    public static partial void LogGetProductsResponse(
        this ILogger logger,
        [LogProperties] GetProductsResponse response);

    [LoggerMessage(LogLevel.Debug, "Getting product with id {ProductId}")]
    public static partial void LogGetProductRequest(
        this ILogger logger, string productId);

    [LoggerMessage(LogLevel.Debug, "Product with id {ProductId} received")]
    public static partial void LogGetProductResponse(
        this ILogger logger,
        string productId,
        [LogProperties] GetProductResponse response);

    [LoggerMessage(LogLevel.Debug, "Product with id {ProductId} is not found")]
    public static partial void LogProductNotFound(this ILogger logger, string productId);

    [LoggerMessage(LogLevel.Debug, "Creating product with name {ProductName}")]
    public static partial void LogCreateProductRequest(
        this ILogger logger,
        string productName,
        [LogProperties] CreateProductRequest request);

    [LoggerMessage(LogLevel.Debug, "Product with name {ProductName} created")]
    public static partial void LogCreateProductResponse(
        this ILogger logger,
        string productName,
        [LogProperties] CreateProductResponse response);

    [LoggerMessage(LogLevel.Debug, "Updating product with id {ProductId}")]
    public static partial void LogUpdateProductRequest(
        this ILogger logger,
        string productId,
        [LogProperties] UpdateProductRequest request);

    [LoggerMessage(LogLevel.Debug, "Product with name {ProductId} updated")]
    public static partial void LogUpdateProductResponse(
        this ILogger logger,
        string productId,
        [LogProperties] UpdateProductResponse response);

    [LoggerMessage(LogLevel.Debug, "Deleting product with id {ProductId}")]
    public static partial void LogDeleteProductRequest(
        this ILogger logger,
        string productId);

    [LoggerMessage(LogLevel.Debug, "Product with name {ProductId} deleted")]
    public static partial void LogDeleteProductResponse(
        this ILogger logger,
        string productId,
        [LogProperties] DeleteProductResponse response);
}