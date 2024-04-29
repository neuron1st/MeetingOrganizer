namespace MeetingOrganizer.Web.Pages.Meetings;

public class MeetingModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? Date { get; set; }
    public string? Image { get; set; }

    public int ParticipantsNumber { get; set; }
    public int LikesNumber { get; set; }
    public int CommentsNumber { get; set; }
}
