using MeetingOrganizer.Context.Entities.Common;

namespace MeetingOrganizer.Context.Entities;

public class Photo : BaseEntity
{
    public string Url { get; set; }

    public int MeetingId { get; set; }
    public virtual Meeting Meeting { get; set; }
}
