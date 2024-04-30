using System.ComponentModel.DataAnnotations;

namespace MeetingOrganizer.Context.Entities;

/// <summary>
/// Represents the roles that a participant can have in a meeting.
/// </summary>
public enum Role
{
    [Display(Name = "Admin")]
    Admin,

    [Display(Name = "Moderator")]
    Moderator,

    [Display(Name = "Participant")]
    Participant
}
