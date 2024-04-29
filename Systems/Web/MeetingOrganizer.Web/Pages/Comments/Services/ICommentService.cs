namespace MeetingOrganizer.Web.Pages.Comments;

public interface ICommentService
{
    Task<IEnumerable<CommentModel>> GetAllByMeeting(Guid meetingId);
    Task<IEnumerable<CommentModel>> GetAllByUser(Guid userId);
    Task<CommentModel> GetComment(Guid commentId);
    Task AddComment(CreateModel model);
    Task EditComment(Guid commentId, UpdateModel model);
    Task DeleteComment(Guid commentId);
}
