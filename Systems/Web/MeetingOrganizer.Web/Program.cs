using Blazored.LocalStorage;
using MeetingOrganizer.Web;
using MeetingOrganizer.Web.Pages.Auth;
using MeetingOrganizer.Web.Pages.Comments;
using MeetingOrganizer.Web.Pages.Meetings;
using MeetingOrganizer.Web.Pages.Participants;
using MeetingOrganizer.Web.Pages.Users;
using MeetingOrganizer.Web.Providers;
using MeetingOrganizer.Web.Services.CommentLike;
using MeetingOrganizer.Web.Services.Configuration;
using MeetingOrganizer.Web.Services.MeetingLike;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Settings.ApiRoot) });

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();

builder.Services.AddScoped<IConfigurationService, ConfigurationService>();

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<ICommentLikeService, CommentLikeService>();
builder.Services.AddScoped<IMeetingLikeService, MeetingLikeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMeetingService, MeetingService>();
builder.Services.AddScoped<IAuthService, AuthService>();

await builder.Build().RunAsync();
