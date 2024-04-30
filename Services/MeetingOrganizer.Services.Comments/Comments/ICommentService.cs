namespace MeetingOrganizer.Services.Comments;

/// <summary>
/// Service for managing comments.
/// </summary>
public interface ICommentService
{
    /// <summary>
    /// Gets all comments for a meeting.
    /// </summary>
    /// <param name="meetingId">The ID of the meeting.</param>
    /// <param name="offset">The offset for pagination.</param>
    /// <param name="limit">The maximum number of comments to return.</param>
    /// <returns>A collection of comment models.</returns>
    Task<IEnumerable<CommentModel>> GetAllByMeetingId(Guid meetingId, int offset = 0, int limit = 10);

    /// <summary>
    /// Gets all comments by a user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="offset">The offset for pagination.</param>
    /// <param name="limit">The maximum number of comments to return.</param>
    /// <returns>A collection of comment models.</returns>
    Task<IEnumerable<CommentModel>> GetAllByUserId(Guid userId, int offset = 0, int limit = 10);

    /// <summary>
    /// Gets a comment by its ID.
    /// </summary>
    /// <param name="id">The ID of the comment.</param>
    /// <returns>The comment model.</returns>
    Task<CommentModel> GetById(Guid id);

    /// <summary>
    /// Creates a new comment.
    /// </summary>
    /// <param name="model">The model containing the comment information.</param>
    /// <returns>The created comment model.</returns>
    Task<CommentModel> Create(CreateModel model);

    /// <summary>
    /// Updates an existing comment.
    /// </summary>
    /// <param name="id">The ID of the comment to update.</param>
    /// <param name="model">The updated comment information.</param>
    Task Update(Guid id, UpdateModel model);

    /// <summary>
    /// Deletes a comment.
    /// </summary>
    /// <param name="id">The ID of the comment to delete.</param>
    /// <param name="userId">The ID of the user performing the delete operation.</param>
    Task Delete(Guid id, Guid userId);
}
