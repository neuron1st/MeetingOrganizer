namespace MeetingOrganizer.Services.Meetings;

/// <summary>
/// Interface for a service handling meetings.
/// </summary>
public interface IMeetingService
{
    /// <summary>
    /// Gets all meetings with optional pagination.
    /// </summary>
    /// <param name="offset">The number of items to skip.</param>
    /// <param name="limit">The maximum number of items to return.</param>
    /// <returns>A collection of MeetingModel objects.</returns>
    Task<IEnumerable<MeetingModel>> GetAll(int offset = 0, int limit = 10);

    /// <summary>
    /// Gets a meeting by its Id.
    /// </summary>
    /// <param name="id">The Id of the meeting to retrieve.</param>
    /// <returns>The MeetingModel object.</returns>
    Task<MeetingModel> GetById(Guid id);

    /// <summary>
    /// Creates a new meeting.
    /// </summary>
    /// <param name="model">The model containing the meeting data.</param>
    /// <returns>The created MeetingModel object.</returns>
    Task<MeetingModel> Create(CreateModel model);

    /// <summary>
    /// Updates an existing meeting.
    /// </summary>
    /// <param name="id">The Id of the meeting to update.</param>
    /// <param name="model">The model containing the updated meeting data.</param>
    Task Update(Guid id, UpdateModel model);

    /// <summary>
    /// Deletes a meeting.
    /// </summary>
    /// <param name="id">The Id of the meeting to delete.</param>
    /// <param name="userId">The Id of the user performing the delete operation.</param>
    Task Delete(Guid id, Guid userId);
}
