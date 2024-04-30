namespace MeetingOrganizer.Services.UserAccount;

/// <summary>
/// Service interface for user account management.
/// </summary>
public interface IUserAccountService
{
    /// <summary>
    /// Checks if the user account service is empty.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, returning true if the service is empty, false otherwise.</returns>
    Task<bool> IsEmpty();

    /// <summary>
    /// Retrieves a user account by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user account.</param>
    /// <returns>A task representing the asynchronous operation, returning the user account model.</returns>
    Task<UserAccountModel> GetById(Guid id);

    /// <summary>
    /// Creates a new user account based on the provided registration model.
    /// </summary>
    /// <param name="model">The registration model containing user account details.</param>
    /// <returns>A task representing the asynchronous operation, returning the created user account model.</returns>
    Task<UserAccountModel> Create(RegisterUserAccountModel model);

    /// <summary>
    /// Sends a confirmation link to the specified email address for verifying the email account.
    /// </summary>
    /// <param name="email">The email address to send the confirmation link to.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendConfirmationLinkAsync(string email);

    /// <summary>
    /// Confirms the email address associated with the provided token and email address.
    /// </summary>
    /// <param name="token">The confirmation token sent to the user's email address.</param>
    /// <param name="email">The user's email address.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ConfirmEmail(string token, string email);
}
