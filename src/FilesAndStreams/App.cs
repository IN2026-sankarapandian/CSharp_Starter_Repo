using FilesAndStreams.Constants;
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
    public static void Main()
    {
        ConsoleUI consoleUI = new ();
        do
        {
            consoleUI.ShowMessage(MessageType.Title, Messages.FileAndStreams);
            consoleUI.ShowMessage(MessageType.Information, Messages.TaskOptions);
            consoleUI.ShowMessage(MessageType.Prompt, Messages.EnterTaskToRun);
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
                    consoleUI.ShowMessage(MessageType.Information, Messages.EnterValidOption);
                    break;
            }
        }
        while (true);
    }
}