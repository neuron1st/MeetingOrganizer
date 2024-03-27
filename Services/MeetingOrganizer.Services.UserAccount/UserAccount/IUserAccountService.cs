namespace MeetingOrganizer.Services.UserAccount;

public interface IUserAccountService
{
    Task<bool> IsEmpty();

    Task<UserAccountModel> Create(RegisterUserAccountModel model);

    Task SendConfirmationLinkAsync(string email);

    Task ConfirmEmail(string token, string email);
}
