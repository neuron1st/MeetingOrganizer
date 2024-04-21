namespace MeetingOrganizer.Services.Comments;

public interface ICommentService
{
    Task<IEnumerable<CommentModel>> GetAllByMeetingId(Guid meetingId, int offset = 0, int limit = 10);
    Task<IEnumerable<CommentModel>> GetAllByUserId(Guid userId, int offset = 0, int limit = 10);
    Task<CommentModel> GetById(Guid id);
    Task<CommentModel> Create(CreateModel model);
    Task Update(Guid id, UpdateModel model);
    Task Delete(Guid id, Guid userId);
}
