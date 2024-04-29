using System.Net.Http.Json;

namespace MeetingOrganizer.Web.Pages.Users;

public class UserService(HttpClient httpClient) : IUserService
{
    public async Task<UserModel> Get()
    {
        var url = $"{Settings.ApiRoot}/v1/Accounts";

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<UserModel>() ?? new();
    }
}
