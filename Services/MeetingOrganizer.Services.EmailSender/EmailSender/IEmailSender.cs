namespace MeetingOrganizer.Services.EmailSender;

public interface IEmailSender
{
    Task SendAsync(EmailModel email);
}
