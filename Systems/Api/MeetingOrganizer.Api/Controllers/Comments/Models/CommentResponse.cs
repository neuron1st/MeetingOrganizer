using AutoMapper;
using MeetingOrganizer.Services.Comments;

namespace MeetingOrganizer.Api.Controllers.Comments;

public class CommentResponse
{
    public Guid Id { get; set; }

    public string Text { get; set; }

    public int Likes { get; set; }

    public Guid UserId { get; set; }

    public Guid MeetingId { get; set; }
}

public class CommentResponseProfile : Profile
{
    public CommentResponseProfile()
    {
        CreateMap<CommentModel, CommentResponse>();
    }
}