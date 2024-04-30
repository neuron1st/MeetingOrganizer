namespace MeetingOrganizer.Services.EmailSender;

/// <summary>
/// Represents an email sender service.
/// </summary>
public interface IEmailSender
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="email">The email model containing email details.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendAsync(EmailModel email);
}
