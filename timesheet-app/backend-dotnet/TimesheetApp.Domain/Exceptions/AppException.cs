using System.Globalization;

namespace TimesheetApp.Domain.Exceptions;

// custom exception class for throwing application specific exceptions (e.g. for validation)
// that can be caught and handled within the application
public class AppException : Exception
{
    public AppException()
        : base()
    {
    }

    public AppException(string message)
        : base(message)
    {
    }

    public AppException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }

    public AppException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
