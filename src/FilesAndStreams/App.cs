using FilesAndStreams.Constants;
using FilesAndStreams.Enums;
using FilesAndStreams.FileServices;
using FilesAndStreams.FormHandlers;
using FilesAndStreams.Tasks;
using FilesAndStreams.UserInterface;
using FilesAndStreams.Validators;

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
        IUserInterface userInterface = new ConsoleUI();
        FileValidator validator = new ();
        FormHandler formHandler = new (userInterface, validator);
        FileService fileService = new ();
        do
        {
            userInterface.ShowMessage(MessageType.Title, Messages.FileAndStreams);
            userInterface.ShowMessage(MessageType.Information, Messages.TaskOptions);
            userInterface.ShowMessage(MessageType.Prompt, Messages.AskUserChoice);
            string? userInput = userInterface.GetInput();
            switch (userInput)
            {
                case "1":
                    new Task1(userInterface, formHandler, fileService).Run();
                    break;
                case "2":
                    new Task2(userInterface, formHandler, fileService).Run();
                    break;
                case "3":
                    new Task3(userInterface, formHandler).Run();
                    break;
                case "4":
                    new Task4(userInterface).Run();
                    break;
                case "5":
                    return;
                default:
                    userInterface.ShowMessage(MessageType.Information, Messages.EnterValidOption);
                    break;
            }
        }
        while (true);
    }
}