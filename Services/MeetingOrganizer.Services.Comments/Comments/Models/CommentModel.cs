using AutoMapper;
using MeetingOrganizer.Context.Entities;

namespace MeetingOrganizer.Services.Comments;

public class CommentModel
{
    public Guid Id { get; set; }

    public string Text { get; set; }

    public int LikesNumber { get; set; }

    public string UserName { get; set; }

    public Guid UserId { get; set; }

    public Guid MeetingId { get; set; }
}

public class CommentModelProfile : Profile
{
    public CommentModelProfile()
    {
        CreateMap<Comment, CommentModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.LikesNumber, opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.MeetingId, opt => opt.MapFrom(src => src.Meeting.Uid));
    }
}
