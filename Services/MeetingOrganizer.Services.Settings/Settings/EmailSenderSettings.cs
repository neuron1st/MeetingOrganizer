namespace MeetingOrganizer.Services.Settings;

public class EmailSenderSettings
{
    public string Host { get; private set; }
    public int Port { get; private set; }
    public bool UseSsl { get; private set; }
    public string Email { get; private set; }
    public string SenderName { get; private set; }
    public string Password { get; private set; }
}
