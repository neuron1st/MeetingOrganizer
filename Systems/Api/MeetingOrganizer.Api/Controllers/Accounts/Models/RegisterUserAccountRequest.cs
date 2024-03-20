using AutoMapper;
using MeetingOrganizer.Services.UserAccount;

namespace MeetingOrganizer.Api.Controllers.Accounts.Models;

public class RegisterUserAccountRequest
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class RegisterUserAccountRequestProfile : Profile
{
    public RegisterUserAccountRequestProfile()
    {
        CreateMap<RegisterUserAccountRequest, RegisterUserAccountModel>();
    }
}
