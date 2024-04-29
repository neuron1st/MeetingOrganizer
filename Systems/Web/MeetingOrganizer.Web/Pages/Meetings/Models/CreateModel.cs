using MeetingOrganizer.Web.Common;

namespace MeetingOrganizer.Web.Pages.Meetings;

public class CreateModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? Date { get; set; }
    public FilePayload Image { get; set; }
}
