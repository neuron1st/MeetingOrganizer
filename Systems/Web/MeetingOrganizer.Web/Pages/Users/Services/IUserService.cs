namespace MeetingOrganizer.Web.Pages.Users;

public interface IUserService
{
    Task<UserModel> Get();
}
