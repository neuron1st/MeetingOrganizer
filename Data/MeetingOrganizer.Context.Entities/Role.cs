using System.ComponentModel.DataAnnotations;

namespace MeetingOrganizer.Context.Entities;

public enum Role
{
    [Display(Name = "Admin")]
    Admin,

    [Display(Name = "Moderator")]
    Moderator,

    [Display(Name = "Participant")]
    Participant
}
