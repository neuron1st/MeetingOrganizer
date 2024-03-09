using MeetingOrganizer.Context.Entities.Common;

namespace MeetingOrganizer.Context.Entities;

public class Meeting : BaseEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }

    public virtual ICollection<Participant> Participants { get; set; }
    public virtual ICollection<Photo> Photos { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<MeetingLike> Likes { get; set; }
}
