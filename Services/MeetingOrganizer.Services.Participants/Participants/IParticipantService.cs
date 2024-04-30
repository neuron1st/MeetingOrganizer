namespace MeetingOrganizer.Services.Participants;

/// <summary>
/// Service interface for managing participants.
/// </summary>
public interface IParticipantService
{
    /// <summary>
    /// Gets all participants for a meeting with pagination.
    /// </summary>
    Task<IEnumerable<ParticipantModel>> GetAllByMeetingId(Guid meetingId, int offset = 0, int limit = 10);

    /// <summary>
    /// Gets all participants for a user with pagination.
    /// </summary>
    Task<IEnumerable<ParticipantModel>> GetAllByUserId(Guid userId, int offset = 0, int limit = 10);

    /// <summary>
    /// Gets a participant by user and meeting ID.
    /// </summary>
    Task<ParticipantModel> GetByUserAndMeetingId(Guid userId, Guid meetingId);

    /// <summary>
    /// Gets a participant by ID.
    /// </summary>
    Task<ParticipantModel> GetById(Guid id);

    /// <summary>
    /// Creates a new participant.
    /// </summary>
    Task<ParticipantModel> Create(CreateModel model);

    /// <summary>
    /// Updates a participant.
    /// </summary>
    Task Update(Guid id, UpdateModel model);

    /// <summary>
    /// Deletes a participant.
    /// </summary>
    Task Delete(Guid id);

    /// <summary>
    /// Notifies all participants of a meeting.
    /// </summary>
    Task NotifyAllParticipants(Guid meetingId, string message);
}
