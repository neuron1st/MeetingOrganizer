namespace MeetingOrganizer.Context.Entities;

/// <summary>
/// Represents a like for a comment entity.
/// </summary>
public class CommentLike
{
    /// <summary>
    /// The identifier of the user who liked the comment.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// The user who liked the comment.
    /// </summary>
    public virtual User User { get; set; }

    /// <summary>
    /// The identifier of the comment that was liked.
    /// </summary>
    public int CommentId { get; set; }

    /// <summary>
    /// The comment that was liked.
    /// </summary>
    public virtual Comment Comment { get; set; }
}
