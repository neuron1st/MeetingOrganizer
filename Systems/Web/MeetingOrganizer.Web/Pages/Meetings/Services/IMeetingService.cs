namespace MeetingOrganizer.Web.Pages.Meetings;

public interface IMeetingService
{
    Task<IEnumerable<MeetingModel>> GetMeetings();
    Task<MeetingModel> GetMeeting(Guid meetingId);
    Task AddMeeting(CreateModel model);
    Task EditMeeting(Guid meetingId, UpdateModel model);
    Task DeleteMeeting(Guid meetingId);
}
