using CSharpAdvanced.Enums;

namespace CSharpAdvanced.UserInterface;

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
    /// <param name="type">Type of the message to show.</param>
    /// <param name="message">Message shown to user.</param>
    public void ShowMessage(MessageType type, string message);
}
