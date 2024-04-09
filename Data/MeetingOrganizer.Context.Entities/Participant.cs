﻿using MeetingOrganizer.Context.Entities.Common;

namespace MeetingOrganizer.Context.Entities;

public class Participant : BaseEntity
{
    public int UserId { get; set; }
    public virtual User User { get; set; }

    public int MeetingId {  get; set; }
    public virtual Meeting Meeting { get; set; }

    public Role Role { get; set; }
}