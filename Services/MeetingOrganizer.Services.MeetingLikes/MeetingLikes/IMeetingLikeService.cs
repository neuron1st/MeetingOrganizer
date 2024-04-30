namespace MeetingOrganizer.Services.MeetingLikes;

/// <summary>
/// Interface for a service handling meeting likes.
/// </summary>
public interface IMeetingLikeService
{
    /// <summary>
    /// Adds a like for a meeting.
    /// </summary>
    /// <param name="model">The model containing the UserId and MeetingId of the like.</param>
    Task AddLike(MeetingLikeModel model);

    /// <summary>
    /// Removes a like for a meeting.
    /// </summary>
    /// <param name="model">The model containing the UserId and MeetingId of the like to remove.</param>
    Task RemoveLike(MeetingLikeModel model);

    /// <summary>
    /// Checks if a user has liked a meeting.
    /// </summary>
    /// <param name="meetingId">The Id of the meeting to check for likes.</param>
    /// <param name="userId">The Id of the user to check for likes.</param>
    /// <returns>True if the user has liked the meeting, false otherwise.</returns>
    Task<bool> CheckLike(Guid meetingId, Guid userId);
}
