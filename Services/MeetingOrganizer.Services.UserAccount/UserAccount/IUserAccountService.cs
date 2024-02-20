namespace MeetingOrganizer.Services.UserAccount;

public interface IUserAccountService
{
    Task<bool> IsEmpty();

    Task<UserAccountModel> Create(RegisterUserAccountModel model);
}
