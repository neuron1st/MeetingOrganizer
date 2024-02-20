namespace MeetingOrganizer.Services.UserAccount;

public class UserAccountModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}
