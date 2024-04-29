
using System.Net.Http.Json;
using System.Reflection;

namespace MeetingOrganizer.Web.Pages.Participants;

public class ParticipantService(HttpClient httpClient) : IParticipantService
{
    public async Task<IEnumerable<ParticipantModel>> GetAllByMeeting(Guid meetingId)
    {
        var url = $"{Settings.ApiRoot}/v1/Participant/meeting/{meetingId}";

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<ParticipantModel>>() ?? new List<ParticipantModel>();
    }

    public async Task<IEnumerable<ParticipantModel>> GetAllByUser(Guid userId)
    {
        var url = $"{Settings.ApiRoot}/v1/Participant/user/{userId}";

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<ParticipantModel>>() ?? new List<ParticipantModel>();
    }

    public async Task<ParticipantModel> GetParticipant(Guid participantId)
    {
        var url = $"{Settings.ApiRoot}/v1/Participant/{participantId}";

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<ParticipantModel>() ?? new();
    }

    public async Task<ParticipantModel> GetByUserAndMeeting(Guid userId, Guid meetingId)
    {
        var url = $"{Settings.ApiRoot}/v1/Participant/{userId}/{meetingId}";

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<ParticipantModel>() ?? new();
    }

    public async Task AddParticipant(CreateModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/Participant";

        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync(url, requestContent);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task EditParticipant(Guid participantId, UpdateModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/Participant/{participantId}";

        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync(url, requestContent);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task DeleteParticipant(Guid participantId)
    {
        var url = $"{Settings.ApiRoot}/v1/Participant/{participantId}";

        var response = await httpClient.DeleteAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}
