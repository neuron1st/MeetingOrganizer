namespace MeetingOrganizer.Context.Settings;

/// <summary>
/// Represents the settings for the database connection.
/// </summary>
public class DbSettings
{
    public string ConnectionString { get; private set; } = null!;
    public DbInitSettings Init { get; private set; }
}
