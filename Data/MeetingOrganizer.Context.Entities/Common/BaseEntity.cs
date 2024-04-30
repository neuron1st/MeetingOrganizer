namespace MeetingOrganizer.Context.Entities.Common;

using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Base entity
/// </summary>
[Index("Uid", IsUnique = true)]
public abstract class BaseEntity
{
    /// <summary>
    /// The identifier for the entity in database.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    /// <summary>
    /// The unique identifier for the entity.
    /// </summary>
    [Required]
    public virtual Guid Uid { get; set; } = Guid.NewGuid();

    /// <summary>
    /// The creation time of the entity.
    /// </summary>
    [Required]
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;
}
