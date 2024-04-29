namespace MeetingOrganizer.Web.Services.MeetingLike;

public interface IMeetingLikeService
{
    Task AddLike(Guid meetingId);
    Task DeleteLike(Guid meetingId);
}
