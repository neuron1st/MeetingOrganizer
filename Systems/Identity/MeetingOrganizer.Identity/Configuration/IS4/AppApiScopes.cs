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
        };
}
