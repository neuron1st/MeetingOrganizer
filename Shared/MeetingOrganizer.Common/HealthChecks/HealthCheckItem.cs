namespace MeetingOrganizer.Common.HealthChecks;

public class HealthCheckItem
{
    public string Status { get; set; }
    public string Component { get; set; }
    public string Description { get; set; }
    public string Duration { get; set; }
}