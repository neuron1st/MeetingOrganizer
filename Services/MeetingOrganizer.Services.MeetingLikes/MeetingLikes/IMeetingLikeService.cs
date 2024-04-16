namespace MeetingOrganizer.Services.MeetingLikes;

public interface IMeetingLikeService
{
    Task AddLike(MeetingLikeModel model);
    Task RemoveLike(MeetingLikeModel model);
}
