using System.Net.Http.Json;

namespace MeetingOrganizer.Web.Pages.Meetings;

public class MeetingService(HttpClient httpClient) : IMeetingService
{
    public async Task<IEnumerable<MeetingModel>> GetMeetings()
    {
        var url = $"{Settings.ApiRoot}/v1/Meeting";

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<MeetingModel>>() ?? new List<MeetingModel>();
    }

    public async Task<MeetingModel> GetMeeting(Guid meetingId)
    {
        var url = $"{Settings.ApiRoot}/v1/Meeting/{meetingId}";

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<MeetingModel>() ?? new();
    }

    public async Task AddMeeting(CreateModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/Meeting";

        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync(url, requestContent);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task EditMeeting(Guid meetingId, UpdateModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/Meeting/{meetingId}";

        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PutAsync(url, requestContent);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task DeleteMeeting(Guid meetingId)
    {
        var url = $"{Settings.ApiRoot}/v1/Meeting/{meetingId}";

        var response = await httpClient.DeleteAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}
