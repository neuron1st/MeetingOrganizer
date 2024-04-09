namespace MeetingOrganizer.Common.Helpers;

public static class EnumValidatorHelper
{
    public static bool IsValidEnum<T>(string value) where T : struct, Enum
    {
        return Enum.TryParse<T>(value, out _);
    }
}
