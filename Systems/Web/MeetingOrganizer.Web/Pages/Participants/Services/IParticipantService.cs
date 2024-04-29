namespace MeetingOrganizer.Web.Pages.Participants;

public interface IParticipantService
{
    Task<IEnumerable<ParticipantModel>> GetAllByMeeting(Guid meetingId);
    Task<IEnumerable<ParticipantModel>> GetAllByUser(Guid userId);
    Task<ParticipantModel> GetParticipant(Guid participantId);
    Task<ParticipantModel> GetByUserAndMeeting(Guid userId, Guid meetingId);
    Task AddParticipant(CreateModel model);
    Task EditParticipant(Guid participantId, UpdateModel model);
    Task DeleteParticipant(Guid participantId);
}
