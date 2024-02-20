using Microsoft.AspNetCore.Identity;

namespace MeetingOrganizer.Common.Exceptions;

/// <summary>
/// Base exception for transferred error in the work process
/// </summary>
public class ProcessException : Exception
{
    public string Code { get; } = null!;

    public string Name { get; } = null!;


    #region Constructors

    public ProcessException()
    {
    }

    public ProcessException(string message) : base(message)
    {
    }

    public ProcessException(Exception inner) : base(inner.Message, inner)
    {
    }

    public ProcessException(string message, Exception inner) : base(message, inner)
    {
    }

    public ProcessException(string code, string message) : base(message)
    {
        Code = code;
    }

    public ProcessException(string code, string message, Exception inner) : base(message, inner)
    {
        Code = code;
    }

    public static void ThrowIf(Func<bool> predicate, string message)
    {
        if (predicate.Invoke())
            throw new ProcessException(message);
    }

    public static void ThrowIfNotSuccessful(IdentityResult result)
    {
        throw new NotImplementedException();
    }

    #endregion
}
