using Dotnet.ArithmeticCalculator.Constants;

namespace Dotnet.ArithmeticCalculator.UserInterface;

/// <summary>
/// Defines a contract for user interface.
/// </summary>
public interface IUserInterface
{
    /// <summary>
    /// Gets user input and returns it.
    /// </summary>
    /// <returns>User's input</returns>
    public string? GetInput();

    /// <summary>
    /// Shows the message to user as a specified type.
    /// </summary>
    /// <param name="message">Message shown to user.</param>
    /// <param name="type">Type of the message to show.</param>
    public void ShowMessage(string message, MessageType type);
}
