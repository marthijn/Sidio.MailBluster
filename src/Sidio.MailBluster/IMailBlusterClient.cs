using Sidio.MailBluster.Requests.Fields;
using Sidio.MailBluster.Requests.Leads;
using Sidio.MailBluster.Responses.Fields;
using Sidio.MailBluster.Responses.Leads;

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
    /// Gets all the fields.
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
    ///Deletes a field.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="DeleteFieldResponse"/>.</returns>
    Task<DeleteFieldResponse> DeleteFieldAsync(
        long id,
        CancellationToken cancellationToken = default);
}