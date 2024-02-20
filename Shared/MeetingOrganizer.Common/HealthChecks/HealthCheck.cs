namespace MeetingOrganizer.Common.HealthChecks;

public class HealthCheck
{
    public string OverallStatus { get; set; }
    public IEnumerable<HealthCheckItem> HealthChecks { get; set; }
    public string TotalDuration { get; set; }
}
