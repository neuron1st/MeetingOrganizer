namespace MeetingOrganizer.Services.Meetings;

public interface IMeetingService
{
    Task<IEnumerable<MeetingModel>> GetAll(int offset = 0, int limit = 10);
    Task<MeetingModel> GetById(Guid id);
    Task<MeetingModel> Create(CreateModel model);
    Task Update(Guid id, UpdateModel model);
    Task Delete(Guid id);
}
