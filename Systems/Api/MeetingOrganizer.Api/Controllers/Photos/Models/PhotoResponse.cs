using AutoMapper;
using MeetingOrganizer.Services.Photos;

namespace MeetingOrganizer.Api.Controllers.Photos;

public class PhotoResponse
{
    public Guid Id { get; set; }

    public string Image { get; set; }

    public Guid MeetingId { get; set; }
}

public class PhotoResponseProfile : Profile
{
    public PhotoResponseProfile()
    {
        CreateMap<PhotoModel, PhotoResponse>();
    }
}