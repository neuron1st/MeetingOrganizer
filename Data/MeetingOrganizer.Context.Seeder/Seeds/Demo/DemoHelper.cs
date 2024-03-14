using MeetingOrganizer.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingOrganizer.Context.Seeder;

public class DemoHelper
{
    public IEnumerable<Meeting> GetMeetings = new List<Meeting>
    {
        new Meeting()
        {
            Uid = Guid.NewGuid(),
            Title = "The Council of Magicians",
            CreationTime = DateTime.UtcNow,
        }
    };
}
