using AutoMapper;
using MeetingOrganizer.Services.Meetings;

namespace MeetingOrganizer.Api.Controllers.Meetings;

public class CreateRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
}

public class CreateRequestProfile : Profile
{
    public CreateRequestProfile()
    {
        CreateMap<CreateRequest, CreateModel>();
    }
}
