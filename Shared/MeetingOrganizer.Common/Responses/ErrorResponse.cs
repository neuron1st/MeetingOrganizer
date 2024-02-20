namespace MeetingOrganizer.Common.Responses;

public class ErrorResponse
{
    public string Code { get; set; }
    public string Message { get; set; }
    public IEnumerable<ErrorResponseFieldInfo> FieldErrors { get; set; }
}
