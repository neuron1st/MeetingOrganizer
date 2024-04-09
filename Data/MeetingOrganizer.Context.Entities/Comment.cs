using MeetingOrganizer.Context.Entities.Common;

namespace MeetingOrganizer.Context.Entities;

public class Comment : BaseEntity
{
    public string Text {  get; set; }

    public int? UserId { get; set; }
    public virtual User User { get; set; }

    public int MeetingId { get; set; }
    public virtual Meeting Meeting { get; set; }

    public virtual ICollection<CommentLike> Likes { get; set; }
}