using AutoMapper;
using MeetingOrganizer.Services.Comments;

namespace MeetingOrganizer.Api.Controllers.Comments;

public class CreateRequest
{
    public string Text { get; set; }

    public Guid MeetingId { get; set; }
}

public class CreateRequestProfile : Profile
{
    public CreateRequestProfile()
    {
        CreateMap<CreateRequest, CreateModel>();
    }
}