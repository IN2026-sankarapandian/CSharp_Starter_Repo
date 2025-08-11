using Dotnet.ArithmeticCalculator.Constants;

namespace Dotnet.ArithmeticCalculator.UserInterface;

/// <summary>
/// Provides operations to interact with user via console.
/// </summary>
public class ConsoleUI
{
    /// <summary>
    /// Gets user input and returns it.
    /// </summary>
    /// <returns>User's input</returns>
    public string? GetInput()
    {
        return Console.ReadLine();
    }

    /// <summary>
    /// Shows the message to user as a specified type.
    /// </summary>
    /// <param name="message">Message shown to user.</param>
    /// <param name="type">Type of the message to show.</param>
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
                Console.ResetColor();
                break;
            case MessageType.Result:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(message);
                Console.ResetColor();
                break;
            case MessageType.Title:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(message);
                Console.ResetColor();
                break;
        }
    }
}
