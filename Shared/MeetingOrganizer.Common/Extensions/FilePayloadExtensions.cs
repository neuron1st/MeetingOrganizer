using MeetingOrganizer.Common.Files;

namespace MeetingOrganizer.Common.Extensions;

public static class FilePayloadExtensions
{
    public static FileData ToFileData(this FilePayload filePayload)
    {
        var content = Convert.FromBase64String(filePayload.Content);

        var extension = Path.GetExtension(filePayload.FileName);

        return new FileData()
        {
            Name = filePayload.FileName,
            Extension = extension,
            Content = content,
        };
    }
}
