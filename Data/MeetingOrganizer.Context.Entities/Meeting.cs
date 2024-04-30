using MeetingOrganizer.Context.Entities.Common;

namespace MeetingOrganizer.Context.Entities;

/// <summary>
/// Represents a meeting entity.
/// </summary>
public class Meeting : BaseEntity
{
    /// <summary>
    /// The title of the meeting.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The description of the meeting.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The date of the meeting.
    /// </summary>
    public DateTime? Date { get; set; }

    /// <summary>
    /// The image associated with the meeting.
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// The collection of participants attending the meeting.
    /// </summary>
    public virtual ICollection<Participant> Participants { get; set; }

    /// <summary>
    /// The collection of photos related to the meeting.
    /// </summary>
    public virtual ICollection<Photo> Photos { get; set; }

    /// <summary>
    /// The collection of comments made on the meeting.
    /// </summary>
    public virtual ICollection<Comment> Comments { get; set; }

    /// <summary>
    /// The collection of likes for the meeting.
    /// </summary>
    public virtual ICollection<MeetingLike> Likes { get; set; }
}
