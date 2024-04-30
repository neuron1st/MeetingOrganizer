namespace MeetingOrganizer.Web.Pages.Photos;

public class PhotoModel
{
    public Guid Id { get; set; }

    public string Image {  get; set; }

    public Guid MeetingId { get; set; }
}
