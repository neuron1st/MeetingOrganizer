using AutoMapper;
using MeetingOrganizer.Context.Entities;

namespace MeetingOrganizer.Services.Meetings;

public class MeetingModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public int ParticipantsNumber { get; set; }
    public int LikesNumber { get; set; }
    public int CommentsNumber { get; set; }
}

public class MeetingModelProfile : Profile
{
    public MeetingModelProfile()
    {
        CreateMap<Meeting, MeetingModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.ParticipantsNumber, opt => opt.MapFrom(src => src.Participants.Count))
            .ForMember(dest => dest.LikesNumber, opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dest => dest.CommentsNumber, opt => opt.MapFrom(src => src.Comments.Count));
    }
}
