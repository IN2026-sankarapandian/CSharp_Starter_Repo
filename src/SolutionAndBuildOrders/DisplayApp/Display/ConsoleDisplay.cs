using DisplayApp.ArithmeticOperations;
using DisplayApp.Enums;

namespace DisplayApp.Display;

/// <summary>
/// Provides operations to display the results to user via console.
/// </summary>
public class ConsoleDisplay : IUserDisplay
{
    /// <inheritdoc/>
    public void ShowMessage(MessageType type, string message)
    {
        switch (type)
        {
            case MessageType.Prompt:
                Console.Write(message);
                break;
            case MessageType.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(message);
                break;
            case MessageType.Result:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                break;
            case MessageType.Title:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(message);
                break;
        }

        Console.ResetColor();
    }
}