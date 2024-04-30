namespace MeetingOrganizer.Context.Entities;

/// <summary>
/// Represents a like for a meeting entity.
/// </summary>
public class MeetingLike
{
    /// <summary>
    /// The identifier of the user who liked the meeting.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// The user who liked the meeting.
    /// </summary>
    public virtual User User { get; set; }

    /// <summary>
    /// The identifier of the meeting that was liked.
    /// </summary>
    public int MeetingId { get; set; }

    /// <summary>
    /// The meeting that was liked.
    /// </summary>
    public virtual Meeting Meeting { get; set; }
}
