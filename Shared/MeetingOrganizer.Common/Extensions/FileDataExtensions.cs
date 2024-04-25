using MeetingOrganizer.Common.Files;

namespace MeetingOrganizer.Common.Extensions;

public static class FileDataExtensions
{
    public static async Task<string> SaveToFile(this FileData fileData, string path)
    {
        var prefix = Guid.NewGuid().Shrink();
        var fileName = $"{prefix}_{fileData.Name}";
        var filePath = Path.Combine(path, fileName);

        await File.WriteAllBytesAsync(filePath, fileData.Content);

        return fileName;
    }

    public static async Task ReadFromFile(this FileData fileData, string filePath)
    {
        if (fileData == null)
            throw new ArgumentNullException(nameof(fileData));

        if (!File.Exists(filePath))
            throw new FileNotFoundException(nameof(filePath));

        fileData.Name = Path.GetFileName(filePath);
        fileData.Extension = Path.GetExtension(filePath);
        fileData.Content = await File.ReadAllBytesAsync(filePath);
    }
}
