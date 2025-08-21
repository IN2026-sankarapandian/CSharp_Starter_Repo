using ConsoleDisplays.Enums;

namespace ConsoleDisplays.Displays;

/// <summary>
/// Defines a contract for user display.
/// </summary>
public interface IConsoleDisplay
{
    /// <summary>
    /// Shows the message to user as a specified type.
    /// </summary>
    /// <param name="message">Message shown to user.</param>
    /// <param name="type">Type of the message to show.</param>
    public void ShowMessage(string message, MessageType type);
}