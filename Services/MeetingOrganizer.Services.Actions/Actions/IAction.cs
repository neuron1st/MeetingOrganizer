using MeetingOrganizer.Services.EmailSender;

namespace MeetingOrganizer.Services.Actions;

public interface IAction
{
    Task SendEmailAsync(EmailModel email);
}
