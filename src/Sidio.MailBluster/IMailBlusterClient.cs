using Sidio.MailBluster.Requests.Fields;
using Sidio.MailBluster.Requests.Leads;
using Sidio.MailBluster.Requests.Products;
using Sidio.MailBluster.Responses.Fields;
using Sidio.MailBluster.Responses.Leads;
using Sidio.MailBluster.Responses.Products;

namespace Sidio.MailBluster;

/// <summary>
/// The MailBluster client interface.
/// </summary>
public interface IMailBlusterClient
{
    /// <summary>
    /// Creates a lead.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="CreateLeadResponse"/>.</returns>
    Task<CreateLeadResponse> CreateLeadAsync(CreateLeadRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a lead.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="GetLeadResponse"/> or <c>null</c> shen the lead is not found.</returns>
    Task<GetLeadResponse?> GetLeadAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a lead.
    /// </summary>
    /// <param name="email">The email address.</param>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An <see cref="UpdateLeadResponse"/>.</returns>
    Task<UpdateLeadResponse> UpdateLeadAsync(string email, UpdateLeadRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a lead.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="DeleteLeadResponse"/>.</returns>
    Task<DeleteLeadResponse> DeleteLeadAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// List all fields.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="GetFieldsResponse"/>.</returns>
    Task<GetFieldsResponse> GetFieldsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a field.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="CreateFieldResponse"/>.</returns>
    Task<CreateFieldResponse> CreateFieldAsync(
        CreateFieldRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a field.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="UpdateFieldResponse"/>.</returns>
    Task<UpdateFieldResponse> UpdateFieldAsync(
        long id,
        UpdateFieldRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a field.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="DeleteFieldResponse"/>.</returns>
    Task<DeleteFieldResponse> DeleteFieldAsync(
        long id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// List all products.
    /// </summary>
    /// <param name="perPage">The items per page (default 10).</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="pageNo">The page number (default 1).</param>
    /// <returns>A <see cref="GetProductsResponse"/>.</returns>
    Task<GetProductsResponse> GetProductsAsync(int? pageNo = null, int? perPage = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a product.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="GetProductResponse"/>.</returns>
    Task<GetProductResponse?> GetProductAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a product.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="CreateProductResponse"/>.</returns>
    Task<CreateProductResponse> CreateProductAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a product.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="UpdateProductResponse"/>.</returns>
    Task<UpdateProductResponse> UpdateProductAsync(
        string id,
        UpdateProductRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a product.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="DeleteFieldResponse"/>.</returns>
    Task<DeleteProductResponse> DeleteProductAsync(string id, CancellationToken cancellationToken = default);
}