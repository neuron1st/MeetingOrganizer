using Blazored.LocalStorage;
using MeetingOrganizer.Web.Pages.Profiles;
using MeetingOrganizer.Web.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace MeetingOrganizer.Web.Pages.Auth;

public class AuthService : IAuthService
{
    private const string LocalStorageAuthTokenKey = "authToken";
    private const string LocalStorageRefreshTokenKey = "refreshToken";

    private readonly HttpClient httpClient;
    private readonly AuthenticationStateProvider authenticationStateProvider;
    private readonly ILocalStorageService localStorage;

    public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
    {
        this.httpClient = httpClient;
        this.authenticationStateProvider = authenticationStateProvider;
        this.localStorage = localStorage;
    }

    public async Task Register(RegisterModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync($"{Settings.ApiRoot}/v1/Accounts", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ProfileResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ProfileResult();
        result.IsSuccessful = response.IsSuccessStatusCode;

        if(!response.IsSuccessStatusCode)
        {
            throw new Exception(String.Join(", ", result.Errors.Select(x => x.Message).ToList()));
        }
    }

    public async Task<LoginResult> Login(LoginModel loginModel)
    {
        var url = $"{Settings.IdentityRoot}/connect/token";

        var request_body = new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", Settings.ClientId),
            new KeyValuePair<string, string>("client_secret", Settings.ClientSecret),
            new KeyValuePair<string, string>("username", loginModel.Email!),
            new KeyValuePair<string, string>("password", loginModel.Password!)
        };

        var requestContent = new FormUrlEncodedContent(request_body);

        var response = await httpClient.PostAsync(url, requestContent);

        var content = await response.Content.ReadAsStringAsync();

        var loginResult = JsonSerializer.Deserialize<LoginResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new LoginResult();
        loginResult.Successful = response.IsSuccessStatusCode;

        if (!response.IsSuccessStatusCode)
        {
            return loginResult;
        }

        await localStorage.SetItemAsync(LocalStorageAuthTokenKey, loginResult.AccessToken);
        await localStorage.SetItemAsync(LocalStorageRefreshTokenKey, loginResult.RefreshToken);

        ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email!);

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.AccessToken);

        return loginResult;
    }

    public async Task Logout()
    {
        await localStorage.RemoveItemAsync(LocalStorageAuthTokenKey);
        await localStorage.RemoveItemAsync(LocalStorageRefreshTokenKey);

        ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsLoggedOut();

        httpClient.DefaultRequestHeaders.Authorization = null;
    }
}
