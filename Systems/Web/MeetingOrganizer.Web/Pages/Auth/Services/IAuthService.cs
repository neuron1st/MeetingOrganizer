namespace MeetingOrganizer.Web.Pages.Auth;

public interface IAuthService
{
    Task Register(RegisterModel registerModel);
    Task<LoginResult> Login(LoginModel loginModel);
    Task Logout();
}
