using AutoMapper;
using MeetingOrganizer.Services.Participants;

namespace MeetingOrganizer.Api.Controllers.Participants;

public class CreateRequest
{
    public Guid UserId { get; set; }
    public Guid MeetingId { get; set; }
    public string Role { get; set; }
}

public class CreateRequestProfile : Profile
{
    public CreateRequestProfile()
    {
        CreateMap<CreateRequest, CreateModel>();
    }
}
