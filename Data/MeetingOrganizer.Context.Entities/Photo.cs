using MeetingOrganizer.Context.Entities.Common;

namespace MeetingOrganizer.Context.Entities;

/// <summary>
/// Represents a photo entity.
/// </summary>
public class Photo : BaseEntity
{
    /// <summary>
    /// The URL of the photo.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// The identifier of the meeting the photo belongs to.
    /// </summary>
    public int MeetingId { get; set; }

    /// <summary>
    /// The meeting the photo belongs to.
    /// </summary>
    public virtual Meeting Meeting { get; set; }
}
