namespace MeetingOrganizer.Web.Pages.Participants;

public class ParticipantModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public string UserName { get; set; }

    public Guid MeetingId { get; set; }
    public string MeetingTitle { get; set; }

    public string Role { get; set; }
}
