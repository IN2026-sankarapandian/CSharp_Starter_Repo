using BoilerMachineApp.BoilerMachines;
using BoilerMachineApp.Enums;

namespace BoilerMachineApp.UserInterface;

/// <summary>
/// Provides operations to interact with user via console.
/// </summary>
public class ConsoleUI : IUserInterface
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleUI"/> class.
    /// </summary>
    public ConsoleUI()
    {
        this.ShowMessage(MessageType.Title, "Boiler Controller Initialized");

        // Reserving space to print the event messages
        Console.Write("\n");
    }

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
            case MessageType.Title:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(message);
                break;
            case MessageType.Prompt:
                Console.Write(message);
                break;
            case MessageType.Success:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                break;
            case MessageType.Error:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                break;
            case MessageType.Information:
                Console.WriteLine(message);
                break;
        }

        Console.ResetColor();
    }

    /// <summary>
    /// Set status message
    /// </summary>
    /// <param name="statusMessage">Status message to set</param>
    public void SetStatus(string statusMessage)
    {
        (int left, int top) = Console.GetCursorPosition();
        Console.SetCursorPosition(0, 1);
        Console.WriteLine(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, 1);
        this.ShowMessage(MessageType.Success, statusMessage);
        Console.SetCursorPosition(left, top);
    }

    /// <inheritdoc/>
    public void Subscribe(BoilerMachine boilerMachine)
    {
        boilerMachine.OnStateChange += this.SetStatus;
    }
}
