namespace MeetingOrganizer.Web.Services.CommentLike;

public interface ICommentLikeService
{
    Task AddLike(Guid commentId);
    Task DeleteLike(Guid commentId);
}
