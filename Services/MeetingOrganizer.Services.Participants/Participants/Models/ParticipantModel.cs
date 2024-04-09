using AutoMapper;
using MeetingOrganizer.Context.Entities;
using MeetingOrganizer.Common.Extensions;

namespace MeetingOrganizer.Services.Participants;

public class ParticipantModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public string UserName { get; set; }

    public Guid MeetingId { get; set; }
    public string MeetingTitle { get; set; }

    public string Role { get; set; }
}

public class ParticipantModelProfile : Profile
{
    public ParticipantModelProfile()
    {
        CreateMap<Participant, ParticipantModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.GetDisplayName()))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.MeetingTitle, opt => opt.MapFrom(src => src.Meeting.Title))
            .ForMember(dest => dest.MeetingId, opt => opt.MapFrom(src => src.Meeting.Uid))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));
    }
}
