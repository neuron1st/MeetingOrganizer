using System.Net.Http.Json;

namespace MeetingOrganizer.Web.Services.MeetingLike;

public class MeetingLikeService(HttpClient httpClient) : IMeetingLikeService
{
    public async Task AddLike(Guid meetingId)
    {
        var url = $"{Settings.ApiRoot}/v1/MeetingLike/{meetingId}/like";

        var requestContent = JsonContent.Create(string.Empty);
        var response = await httpClient.PostAsync(url, requestContent);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task DeleteLike(Guid meetingId)
    {
        var url = $"{Settings.ApiRoot}/v1/MeetingLike/{meetingId}/unlike";

        var response = await httpClient.DeleteAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}