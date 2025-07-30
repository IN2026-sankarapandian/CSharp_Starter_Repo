namespace ErrorHandling.CustomExceptions;

public class InvalidUserInputException : Exception
{
    public InvalidUserInputException()
    {
    }

    public InvalidUserInputException(string? message)
        : base(message)
    {
    }
    public InvalidUserInputException(string? message, Exception? innerException)
        :base(message, innerException)
    {
    }
}
