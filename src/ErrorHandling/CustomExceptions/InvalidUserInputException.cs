namespace ErrorHandling.CustomExceptions;

/// <summary>
/// The exception that is shown when user gives invalid input.
/// </summary>
public class InvalidUserInputException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidUserInputException"/> class.
    /// </summary>
    public InvalidUserInputException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidUserInputException"/> class.
    /// </summary>
    /// <param name="message">Message for the exception.</param>
    public InvalidUserInputException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidUserInputException"/> class.
    /// </summary>
    /// <param name="message">Message for the exception.</param>
    /// <param name="innerException">Inner exception.</param>
    public InvalidUserInputException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
