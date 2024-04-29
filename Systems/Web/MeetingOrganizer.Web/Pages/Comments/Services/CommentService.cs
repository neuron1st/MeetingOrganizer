using System.Net.Http.Json;

namespace MeetingOrganizer.Web.Pages.Comments;

public class CommentService(HttpClient httpClient) : ICommentService
{
    public async Task<IEnumerable<CommentModel>> GetAllByMeeting(Guid meetingId)
    {
        var url = $"{Settings.ApiRoot}/v1/Comment/meeting/{meetingId}";

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<CommentModel>>() ?? new List<CommentModel>();
    }

    public async Task<IEnumerable<CommentModel>> GetAllByUser(Guid userId)
    {
        var url = $"{Settings.ApiRoot}/v1/Comment/user/{userId}";

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<CommentModel>>() ?? new List<CommentModel>();
    }

    public async Task<CommentModel> GetComment(Guid commentId)
    {
        var url = $"{Settings.ApiRoot}/v1/Comment/{commentId}";

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<CommentModel>() ?? new CommentModel();
    }

    public async Task AddComment(CreateModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/Comment";

        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync(url, requestContent);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task EditComment(Guid commentId, UpdateModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/Comment/{commentId}";

        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync(url, requestContent);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task DeleteComment(Guid commentId)
    {
        var url = $"{Settings.ApiRoot}/v1/Comment/{commentId}";

        var response = await httpClient.DeleteAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}
