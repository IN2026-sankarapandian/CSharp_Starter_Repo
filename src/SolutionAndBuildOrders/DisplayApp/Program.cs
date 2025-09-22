using DisplayApp.Display;
using DisplayApp.Enums;

namespace DisplayApp;

/// <summary>
/// Handles all the display operations.
/// </summary>
public class Program
{
    /// <summary>
    /// It is a entry point of the display app.
    /// </summary>
    public static void Main()
    {
        IUserDisplay consoleDisplay = new ConsoleDisplay();
        consoleDisplay.ShowMessage(MessageType.Title, "Display app");
        Console.ReadKey();
    }
}