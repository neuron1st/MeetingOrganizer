namespace MeetingOrganizer.Services.CommentLikes;

/// <summary>
/// Service for managing comment likes.
/// </summary>
public interface ICommentLikeService
{
    /// <summary>
    /// Adds a like for a comment.
    /// </summary>
    /// <param name="model">The model containing the user and comment information.</param>
    Task AddLike(CommentLikeModel model);

    /// <summary>
    /// Removes a like for a comment.
    /// </summary>
    /// <param name="model">The model containing the user and comment information.</param>
    Task RemoveLike(CommentLikeModel model);

    /// <summary>
    /// Checks if a user has liked a comment.
    /// </summary>
    /// <param name="commentId">The ID of the comment.</param>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>True if the user has liked the comment; otherwise, false.</returns>
    Task<bool> CheckLike(Guid commentId, Guid userId);
}
