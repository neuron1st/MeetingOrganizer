namespace MeetingOrganizer.Services.Participants;

public interface IParticipantService
{
    Task<IEnumerable<ParticipantModel>> GetAllByMeetingId(Guid meetingId, int offset = 0, int limit = 10);
    Task<IEnumerable<ParticipantModel>> GetAllByUserId(Guid userId, int offset = 0, int limit = 10);
    Task<ParticipantModel> GetById(Guid id);
    Task<ParticipantModel> Create(CreateModel model);
    Task Update(Guid id, UpdateModel model);
    Task Delete(Guid id);
}
