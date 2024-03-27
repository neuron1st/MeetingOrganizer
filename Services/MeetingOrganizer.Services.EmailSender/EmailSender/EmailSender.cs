using MeetingOrganizer.Services.Settings;
using Microsoft.Extensions.Logging;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace MeetingOrganizer.Services.EmailSender;

public class EmailSender : IEmailSender
{
    private readonly EmailSenderSettings settings;
    private readonly ILogger<EmailSender> logger;

    public EmailSender(EmailSenderSettings settings, ILogger<EmailSender> logger)
    {
        this.settings = settings;
        this.logger = logger;
    }

    public async Task SendAsync(EmailModel model)
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress(settings.SenderName, settings.Email));
        email.To.Add(MailboxAddress.Parse(model.Email));
        email.Sender = MailboxAddress.Parse(settings.Email);
        email.Subject = model.Subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = model.Message
        };


        using var smtp = new SmtpClient();
        try
        {
            await smtp.ConnectAsync(settings.Host, settings.Port, settings.UseSsl);
            await smtp.AuthenticateAsync(settings.Email, settings.Password);
            await smtp.SendAsync(email);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send email");
            throw;
        }
        finally
        {

            if (smtp.IsConnected)
            {
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
