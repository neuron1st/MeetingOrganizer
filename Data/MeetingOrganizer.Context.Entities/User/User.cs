namespace MeetingOrganizer.Context.Entities;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// The identifier for the entity in the database.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EntryId { get; set; }

    /// <summary>
    /// The full name of the user.
    /// </summary>
    public string FullName { get; set; } = null!;

    /// <summary>
    /// The status of the user.
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// The collection of participants associated with the user.
    /// </summary>
    public virtual ICollection<Participant> Participants { get; set; }

    /// <summary>
    /// The collection of comments made by the user.
    /// </summary>
    public virtual ICollection<Comment> Comments { get; set; }

    /// <summary>
    /// The collection of comment likes made by the user.
    /// </summary>
    public virtual ICollection<CommentLike> CommentLikes { get; set; }

    /// <summary>
    /// The collection of meeting likes made by the user.
    /// </summary>
    public virtual ICollection<MeetingLike> MeetingLikes { get; set; }
}
