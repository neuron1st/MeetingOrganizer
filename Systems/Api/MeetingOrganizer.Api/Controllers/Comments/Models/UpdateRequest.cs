using AutoMapper;
using MeetingOrganizer.Services.Comments;

namespace MeetingOrganizer.Api.Controllers.Comments;

public class UpdateRequest
{
    public string Text { get; set; }
}

public class UpdateRequestProfile : Profile
{
    public UpdateRequestProfile()
    {
        CreateMap<UpdateRequest, UpdateModel>();
    }
}