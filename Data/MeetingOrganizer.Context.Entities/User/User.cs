namespace MeetingOrganizer.Context.Entities.User;

using Microsoft.AspNetCore.Identity;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class User : IdentityUser<Guid>
{
    public string FullName { get; set; } = null!;
    public UserStatus Status { get; set; }
}
