namespace MeetingOrganizer.Context.Settings;

/// <summary>
/// Settings for initializing the database.
/// </summary>
public class DbInitSettings
{
    /// <summary>
    /// Gets a value indicating whether demo data should be added to the database.
    /// </summary>
    public bool AddDemoData { get; private set; }

    /// <summary>
    /// Gets a value indicating whether an administrator account should be added to the database.
    /// </summary>
    public bool AddAdministrator { get; private set; }

    /// <summary>
    /// Gets the credentials for the administrator account.
    /// </summary>
    public UserCredentials Administrator { get; private set; }
}

/// <summary>
/// Represents user credentials.
/// </summary>
public class UserCredentials
{
    /// <summary>
    /// Gets the name of the user.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the email address of the user.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Gets the password of the user.
    /// </summary>
    public string Password { get; private set; }
}
