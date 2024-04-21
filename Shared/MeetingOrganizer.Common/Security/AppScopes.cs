using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingOrganizer.Common.Security;

public static class AppScopes
{
    public const string MeetingsRead = "meetings_read";
    public const string MeetingsWrite = "meetings_write";
    public const string CommentsRead = "comments_read";
    public const string CommentsWrite = "comments_write";
    public const string ParticipantsRead = "participants_read";
    public const string ParticipantsWrite = "participants_write";
}
