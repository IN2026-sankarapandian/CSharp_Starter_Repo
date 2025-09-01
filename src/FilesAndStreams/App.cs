using FilesAndStreams.Enums;
using FilesAndStreams.Tasks;
using FilesAndStreams.UserInterface;

namespace FilesAndStreams;

/// <summary>
/// Demonstrates the files and streams in c sharp.
/// </summary>
public class App
{
    /// <summary>
    /// Its an entry point of files and streams app.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task Main()
    {
        ConsoleUI consoleUI = new ();
        do
        {
            consoleUI.ShowMessage(MessageType.Title, "Files and streams");
            consoleUI.ShowMessage(MessageType.Information, "1. Task 1\n2. Task 2\n3. Task 3\n4. Task 4\n5. Exit");
            consoleUI.ShowMessage(MessageType.Prompt, "Enter which task to run : ");
            string? userInput = consoleUI.GetInput();
            switch (userInput)
            {
                case "1":
                    new Task1(consoleUI).Run();
                    break;
                case "2":
                    new Task2(consoleUI).Run();
                    break;
                case "3":
                    new Task3(consoleUI).Run();
                    break;
                case "4":
                    new Task4(consoleUI).Run();
                    break;
                case "5":
                    return;
                default:
                    consoleUI.ShowMessage(MessageType.Information, "Enter a valid option !");
                    break;
            }
        }
        while (true);
    }
}