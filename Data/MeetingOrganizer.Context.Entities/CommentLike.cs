using MeetingOrganizer.Context.Entities.Common;

namespace MeetingOrganizer.Context.Entities;

public class CommentLike : BaseEntity
{
    public int? UserId { get; set; }
    public virtual User User { get; set; }

    public int CommentId { get; set; }
    public virtual Comment Comment { get; set; }
}