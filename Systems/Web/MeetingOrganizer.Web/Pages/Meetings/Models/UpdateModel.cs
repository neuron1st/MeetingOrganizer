using MeetingOrganizer.Web.Common;

namespace MeetingOrganizer.Web.Pages.Meetings;

public class UpdateModel
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? Date { get; set; }
    public FilePayload? Image { get; set; }
}
