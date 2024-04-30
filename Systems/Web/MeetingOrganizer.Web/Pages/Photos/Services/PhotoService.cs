using System.Net.Http.Json;

namespace MeetingOrganizer.Web.Pages.Photos;

public class PhotoService(HttpClient httpClient) : IPhotoService
{
    public async Task<IEnumerable<PhotoModel>> GetPhotos()
    {
        var url = $"{Settings.ApiRoot}/v1/Photo";

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<PhotoModel>>() ?? new List<PhotoModel>();
    }

    public async Task<IEnumerable<PhotoModel>> GetPhotosByMeeting(Guid meetingId)
    {
        var url = $"{Settings.ApiRoot}/v1/Photo/meeting/{meetingId}";

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<PhotoModel>>() ?? new List<PhotoModel>();
    }

    public async Task<PhotoModel> GetPhoto(Guid photoId)
    {
        var url = $"{Settings.ApiRoot}/v1/Photo/{photoId}";

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<PhotoModel>() ?? new();
    }

    public async Task AddPhoto(CreateModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/Photo";

        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync(url, requestContent);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task DeletePhoto(Guid photoId)
    {
        var url = $"{Settings.ApiRoot}/v1/Photo/{photoId}";

        var response = await httpClient.DeleteAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}
