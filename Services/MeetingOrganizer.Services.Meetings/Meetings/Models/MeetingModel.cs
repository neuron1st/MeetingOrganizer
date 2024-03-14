using AutoMapper;
using MeetingOrganizer.Context.Entities;

namespace MeetingOrganizer.Services.Meetings;

public class MeetingModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
}

public class MeetingModelProfile : Profile
{
    public MeetingModelProfile()
    {
        CreateMap<Meeting, MeetingModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid));
    }
}
