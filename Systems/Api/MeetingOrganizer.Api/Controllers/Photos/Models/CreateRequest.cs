using AutoMapper;
using MeetingOrganizer.Common.Extensions;
using MeetingOrganizer.Common.Files;
using MeetingOrganizer.Services.Photos;

namespace MeetingOrganizer.Api.Controllers.Photos;

public class CreateRequest
{
    public FilePayload Image { get; set; }

    public Guid MeetingId {  get; set; }
}

public class CreateRequestProfile : Profile
{
    public CreateRequestProfile()
    {
        CreateMap<CreateRequest, CreateModel>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.ToFileData()));
    }
}
