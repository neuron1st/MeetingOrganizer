namespace MeetingOrganizer.Context.Entities.Common;

/// <summary>
/// Base entity
/// </summary>
public abstract class BaseEntity
{
    public virtual int Id { get; set; }
    public virtual Guid Uid { get; set; } = Guid.NewGuid();
}
