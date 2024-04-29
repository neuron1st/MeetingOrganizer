namespace MeetingOrganizer.Services.CommentLikes;

public interface ICommentLikeService
{
    Task AddLike(CommentLikeModel model);
    Task RemoveLike(CommentLikeModel model);
    Task<bool> CheckLike(Guid commentId, Guid userId);
}
