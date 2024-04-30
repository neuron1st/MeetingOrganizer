using MeetingOrganizer.Services.EmailSender;
using System.Threading.Tasks;

namespace MeetingOrganizer.Services.Actions;

/// <summary>
/// Represents an action that can be performed, such as sending an email.
/// </summary>
public interface IAction
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="email">The email model containing the email details.</param>
    Task SendEmailAsync(EmailModel email);
}
