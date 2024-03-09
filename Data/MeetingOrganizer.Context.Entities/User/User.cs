namespace MeetingOrganizer.Context.Entities;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class User : IdentityUser<Guid>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EntryId { get; set; }

    public string FullName { get; set; } = null!;
    public UserStatus Status { get; set; }

    public virtual ICollection<Participant> Participants { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }

    public virtual ICollection<CommentLike> CommentLikes { get; set; }

    public virtual ICollection<MeetingLike> MeetingLikes { get; set; }
}
