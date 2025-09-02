using DisplayApp.Display;
using DisplayApp.Enums;

namespace GreetingsApp;

/// <summary>
/// Greets the user.
/// </summary>
public class Program
{
    /// <summary>
    /// Its an entry point of greeting app.
    /// </summary>
    public static void Main()
    {
        IUserDisplay consoleDisplay = new ConsoleDisplay();
        consoleDisplay.ShowMessage(MessageType.Title, "Welcome");
        Console.ReadKey();
    }
}