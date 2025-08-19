using Dotnet.ArithmeticCalculator.Enums;

namespace Dotnet.ArithmeticCalculator.UserInterface;

/// <summary>
/// Provides operations to interact with user via console.
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
