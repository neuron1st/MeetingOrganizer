using AutoMapper;
using MeetingOrganizer.Services.Participants;

namespace MeetingOrganizer.Api.Controllers.Participants;

public class UpdateRequest
{
    public string Role { get; set; }
}

public class UpdateRequestProfile : Profile
{
    public UpdateRequestProfile()
    {
        CreateMap<UpdateRequest, UpdateModel>();
    }
}