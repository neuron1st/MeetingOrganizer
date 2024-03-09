using MeetingOrganizer.Context.Entities.Common;

namespace MeetingOrganizer.Context.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<Participant> Participants { get; set; }
}
