namespace MeetingOrganizer.Context.Entities;

public class CommentLike
{
    public int UserId { get; set; }
    public virtual User User { get; set; }

    public int CommentId { get; set; }
    public virtual Comment Comment { get; set; }
}