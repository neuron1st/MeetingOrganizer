using MeetingOrganizer.Web.Common;

namespace MeetingOrganizer.Web.Pages.Photos;

public class CreateModel
{
    public FilePayload Image { get; set; }

    public Guid MeetingId { get; set; }
}
