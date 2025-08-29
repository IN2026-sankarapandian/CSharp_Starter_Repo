using CollectionsAndGenerics.Enums;
using CollectionsAndGenerics.UserInterfaces;

namespace CollectionsAndGenerics.UserInterface;

/// <summary>
/// Provide methods to interact with user via console UI.
/// </summary>
public class ConsoleUI : IUserInterface
{
    /// <inheritdoc/>
    public string? GetInput()
    {
        return Console.ReadLine();
    }

    /// <inheritdoc/>
    public void ShowMessage(MessageType type, string message)
    {
        switch (type)
        {
            case MessageType.Prompt:
                Console.Write(message);
                break;
            case MessageType.Information:
                Console.WriteLine(message);
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
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(message);
                break;
        }

        Console.ResetColor();
    }
}
