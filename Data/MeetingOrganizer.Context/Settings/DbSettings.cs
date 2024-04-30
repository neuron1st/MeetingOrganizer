namespace MeetingOrganizer.Context.Settings;

/// <summary>
/// Represents the settings for the database connection.
/// </summary>
public class DbSettings
{
    /// <summary>
    /// Gets or sets the connection string for the database.
    /// </summary>
    public string ConnectionString { get; private set; } = null!;

    /// <summary>
    /// Gets or sets the initialization settings for the database.
    /// </summary>
    public DbInitSettings Init { get; private set; }
}
