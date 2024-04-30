using MeetingOrganizer.Services.Settings;
using MeetingOrganizer.Services.Logger;
using MeetingOrganizer.Services.UserAccount;
using MeetingOrganizer.Context.Seeder;
using MeetingOrganizer.Services.Meetings;
using MeetingOrganizer.Services.Cache;
using MeetingOrganizer.Services.RabbitMq;
using MeetingOrganizer.Services.EmailSender;
using MeetingOrganizer.Services.Participants;
using MeetingOrganizer.Services.Comments;
using MeetingOrganizer.Services.CommentLikes;
using MeetingOrganizer.Services.MeetingLikes;
using MeetingOrganizer.Services.Actions;
using MeetingOrganizer.Services.Photos;

namespace MeetingOrganizer.Api;

/// <summary>
/// API services bootstrapper
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Main to register app services
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddMainSettings()
            .AddLogSettings()
            .AddSwaggerSettings()
            .AddIdentitySettings()
            .AddCacheSettings()
            .AddRabbitMqSettings()
            .AddAppLogger()
            .AddCache()
            .AddRabbitMq()
            .AddActions()
            .AddDbSeeder()
            .AddMeetingService()
            .AddParticipantService()
            .AddUserAccountService()
            .AddCommentService()
            .AddCommentLikeService()
            .AddMeetingLikeService()
            .AddPhotoService()
            ;

        return services;
    }
}
