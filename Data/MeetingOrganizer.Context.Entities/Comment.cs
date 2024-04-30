using MeetingOrganizer.Context.Entities.Common;

namespace MeetingOrganizer.Context.Entities;

/// <summary>
/// Represents a comment entity.
/// </summary>
public class Comment : BaseEntity
{
    /// <summary>
    /// The text of the comment.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// The identifier of the user who made the comment.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// The user who made the comment.
    /// </summary>
    public virtual User User { get; set; }

    /// <summary>
    /// The identifier of the meeting the comment belongs to.
    /// </summary>
    public int MeetingId { get; set; }

    /// <summary>
    /// The meeting the comment belongs to.
    /// </summary>
    public virtual Meeting Meeting { get; set; }

    /// <summary>
    /// The collection of likes for the comment.
    /// </summary>
    public virtual ICollection<CommentLike> Likes { get; set; }
}
