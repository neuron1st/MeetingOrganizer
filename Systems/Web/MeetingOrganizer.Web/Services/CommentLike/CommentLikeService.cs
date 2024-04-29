using System.Net.Http.Json;

namespace MeetingOrganizer.Web.Services.CommentLike;

public class CommentLikeService(HttpClient httpClient) : ICommentLikeService
{
    public async Task AddLike(Guid commenId)
    {
        var url = $"{Settings.ApiRoot}/v1/CommentLike/{commenId}/like";

        var requestContent = JsonContent.Create(string.Empty);
        var response = await httpClient.PostAsync(url, requestContent);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task DeleteLike(Guid commenId)
    {
        var url = $"{Settings.ApiRoot}/v1/CommentLike/{commenId}/unlike";

        var response = await httpClient.DeleteAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}
