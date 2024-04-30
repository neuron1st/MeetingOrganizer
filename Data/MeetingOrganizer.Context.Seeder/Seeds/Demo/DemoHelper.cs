using MeetingOrganizer.Context.Entities;

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
