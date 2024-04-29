namespace MeetingOrganizer.Web.Pages.Comments;

public class CommentModel
{
    public Guid Id { get; set; }

    public string Text { get; set; }

    public int LikesNumber { get; set; }

    public string UserName { get; set; }

    public bool IsLiked { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid UserId { get; set; }

    public Guid MeetingId { get; set; }
}
