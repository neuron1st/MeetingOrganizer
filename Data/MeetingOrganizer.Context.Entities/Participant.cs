using MeetingOrganizer.Context.Entities.Common;

namespace MeetingOrganizer.Context.Entities;

/// <summary>
/// Represents a participant entity.
/// </summary>
public class Participant : BaseEntity
{
    /// <summary>
    /// The identifier of the user who is a participant.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// The user who is a participant.
    /// </summary>
    public virtual User User { get; set; }

    /// <summary>
    /// The identifier of the meeting the participant belongs to.
    /// </summary>
    public int MeetingId { get; set; }

    /// <summary>
    /// The meeting the participant belongs to.
    /// </summary>
    public virtual Meeting Meeting { get; set; }

    /// <summary>
    /// The role of the participant.
    /// </summary>
    public Role Role { get; set; }
}
