using ConsoleDisplays.Enums;

namespace ConsoleDisplays.Displays;

/// <summary>
/// Provides operations to display to user via console.
/// </summary>
public class ConsoleDisplay : IConsoleDisplay
{
    /// <inheritdoc/>
    public void ShowMessage(string message, MessageType type)
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