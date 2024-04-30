namespace MeetingOrganizer.Web.Pages.Photos;

public interface IPhotoService
{
    Task<IEnumerable<PhotoModel>> GetPhotos();
    Task<IEnumerable<PhotoModel>> GetPhotosByMeeting(Guid meetingId);
    Task<PhotoModel> GetPhoto(Guid photoId);
    Task AddPhoto(CreateModel model);
    Task DeletePhoto(Guid photoId);
}
