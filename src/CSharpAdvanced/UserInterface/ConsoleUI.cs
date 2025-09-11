using CSharpAdvanced.Enums;

namespace CSharpAdvanced.UserInterface;

/// <summary>
/// Provides operations to interact with user via console.
/// </summary>
public class ConsoleUI : IUserInterface
{
    /// <inheritdoc/>
    public string? GetInput() => Console.ReadLine();

    /// <inheritdoc/>
    public void ShowMessage(MessageType type, string message)
    {
        message = message.Replace("\\n", Environment.NewLine);
        switch (type)
        {
            case MessageType.Prompt:
                Console.Write(message);
                break;
            case MessageType.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(message);
                break;
            case MessageType.Highlight:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                break;
            case MessageType.Title:
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(message);
                break;
            case MessageType.Information:
                Console.WriteLine(message);
                break;
        }

        Console.ResetColor();
    }
}
