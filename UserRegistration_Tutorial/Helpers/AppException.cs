using System.Globalization;

namespace UserRegistration_Tutorial.Helpers;

public abstract class AppException : Exception
{
    protected AppException()
    {
    }

    protected AppException(string message) : base(message)
    {
    }

    protected AppException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}