namespace MeetingOrganizer.Services.EmailSender;

/// <summary>
/// Represents an email message.
/// </summary>
public class EmailModel
{
    /// <summary>
    /// The email address.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// The subject of the email.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// The message body of the email.
    /// </summary>
    public string Message { get; set; }
}
