using Dotnet.ArithmeticCalculator.Constants;

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
