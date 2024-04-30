namespace MeetingOrganizer.Services.Photos;

public interface IPhotoService
{
    Task<IEnumerable<PhotoModel>> GetAll();
    Task<IEnumerable<PhotoModel>> GetByMeeting(Guid meetingId);
    Task<PhotoModel> GetById(Guid id);
    Task<PhotoModel> Create(CreateModel model);
    Task Delete(Guid id, Guid userId);
}
