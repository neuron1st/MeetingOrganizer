using Duende.IdentityServer.Models;
using MeetingOrganizer.Common.Security;

namespace MeetingOrganizer.Identity.Configuration;

public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(AppScopes.MeetingsRead, "Access to meetings API - Read data"),
            new ApiScope(AppScopes.MeetingsWrite, "Access to meetings API - Write, edit or delete data"),
            new ApiScope(AppScopes.CommentsRead, "Access to comments API - Read data"),
            new ApiScope(AppScopes.CommentsWrite, "Access to comments API - Write, edit or delete data"),
            new ApiScope(AppScopes.ParticipantsRead, "Access to participants API - Read data"),
            new ApiScope(AppScopes.ParticipantsWrite, "Access to participants API - Write, edit or delete data"),
        };
}
