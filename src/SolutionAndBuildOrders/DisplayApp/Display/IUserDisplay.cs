using DisplayApp.Enums;

namespace DisplayApp.Display;

/// <summary>
/// Defines a contract for console display.
/// </summary>
public interface IUserDisplay
{
    /// <summary>
    /// Shows the message to user as a specified type.
    /// </summary>
    /// <param name="type">Type of the message to show.</param>
    /// <param name="message">Message shown to user.</param>
    public void ShowMessage(MessageType type, string message);
}