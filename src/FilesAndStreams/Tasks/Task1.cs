using FilesAndStreams.Constants;
using FilesAndStreams.Enums;
using FilesAndStreams.FileServices;
using FilesAndStreams.FormHandlers;
using FilesAndStreams.UserInterface;

namespace FilesAndStreams.Tasks;

/// <summary>
/// Demonstrates the how different streams work in synchronous mode.
/// </summary>
public class Task1
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandler _formHandler;
    private readonly FileService _fileService;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task1"/> class.
    /// </summary>
    /// <param name="userInterface">Gives access to UI</param>
    /// <param name="formHandler">Get data from user.</param>
    /// <param name="fileService">Provide methods to handle files</param>
    public Task1(IUserInterface userInterface, FormHandler formHandler, FileService fileService)
    {
        this._userInterface = userInterface;
        this._formHandler = formHandler;
        this._fileService = fileService;
    }

    /// <summary>
    /// Its an entry point for <see cref="Task1"/>
    /// </summary>
    public void Run() => this.HandleMenu();

    /// <summary>
    /// Handles the menu for task 1.
    /// </summary>
    public void HandleMenu()
    {
        do
        {
            this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.TaskTitle, 1));
            this._userInterface.ShowMessage(MessageType.Information, Messages.Task1MenuPrompt);
            string userChoice = this._formHandler.GetUserInput(Messages.AskUserChoice);
            try
            {
                switch (userChoice)
                {
                    case "1":
                        this.HandleCreateSampleFiles();
                        break;
                    case "2":
                        this.HandleReadSampleFiles();
                        break;
                    case "3":
                        this.HandleCreateFilteredFile();
                        break;
                    case "4":
                        return;
                    default:
                        this._userInterface.ShowMessage(MessageType.Information, Messages.EnterValidOption);
                        break;
                }
            }
            catch (IOException ex)
            {
                this._userInterface.ShowMessage(MessageType.Warning, string.Format(Messages.PromptErrorAndGoBack, ex.Message));
                this._userInterface.GetInput();
            }
        }
        while (true);
    }

    /// <summary>
    /// Handles creating sample files needed for the task.
    /// </summary>
    private void HandleCreateSampleFiles()
    {
        this._userInterface.ShowMessage(MessageType.Title, Messages.Create);
        string sampleFileSavePath = this._formHandler.GetTxtFileSavePath(Messages.EnterPathToSaveFile);
        string taskName = string.Format(Messages.WritingFile, Path.GetFileName(sampleFileSavePath));
        this._fileService.CreateLargeTextFile(sampleFileSavePath, FileResources.TargetSize, (progress, elapsedTime)
            => this._userInterface.DrawProgressBar(taskName, progress, elapsedTime));
        this._userInterface.ShowMessage(MessageType.Warning, Messages.PressEnterToExit);
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Handles reading sample files with different streams
    /// </summary>
    private void HandleReadSampleFiles()
    {
        this._userInterface.ShowMessage(MessageType.Title, Messages.Read);
        string sampleFileSavePath = this._formHandler.GetTxtFilePath(Messages.EnterPathToReadFile);
        string taskNameForFileStream = string.Format(Messages.ReadingFileWithFileStream, Path.GetFileName(sampleFileSavePath));
        string taskNameForBufferedStream = string.Format(Messages.ReadingFileWithBufferedStream, Path.GetFileName(sampleFileSavePath));
        this._fileService.ReadFileInChunks(FileReader.FileStream, sampleFileSavePath, (progress, elapsedTime)
            => this._userInterface.DrawProgressBar(
                string.Format(taskNameForFileStream, nameof(FileStream)), progress, elapsedTime));
        this._fileService.ReadFileInChunks(FileReader.BufferedStream, sampleFileSavePath, (progress, elapsedTime)
            => this._userInterface.DrawProgressBar(
                string.Format(taskNameForBufferedStream, nameof(BufferedStream)), progress, elapsedTime));
        this._userInterface.ShowMessage(MessageType.Warning, Messages.PressEnterToExit);
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Handles processing data.
    /// </summary>
    private void HandleCreateFilteredFile()
    {
        this._userInterface.ShowMessage(MessageType.Title, Messages.Process);
        string sampleFilePath = this._formHandler.GetTxtFilePath(Messages.EnterPathToFilterFile);
        string filterFilePath = this._formHandler.GetTxtFileSavePath(Messages.EnterPathToSaveFilteredFile);
        decimal temperatureThreshold = this._formHandler.GetTemperatureThreshold(Messages.EnterTemperatureThresholdValue);
        string readingTaskName = string.Format(Messages.ReadingFileForFiltering, Path.GetFileName(sampleFilePath));
        string filteringTaskName = string.Format(Messages.FilteringFile, Path.GetFileName(sampleFilePath));
        string writingTaskName = string.Format(Messages.WritingFilteredFile, Path.GetFileName(filterFilePath));
        string content = this._fileService.ReadFile(sampleFilePath, (progress, elapsedTime)
            => this._userInterface.DrawProgressBar(readingTaskName, progress, elapsedTime));
        string filteredContent = this._fileService.FilterByTemperature(content, 100, (progress, elapsedTime)
            => this._userInterface.DrawProgressBar(filteringTaskName, progress, elapsedTime));
        this._fileService.WriteData(filterFilePath, filteredContent, (progress, elapsedTime)
            => this._userInterface.DrawProgressBar(writingTaskName, progress, elapsedTime));
        this._userInterface.ShowMessage(MessageType.Warning, Messages.PressEnterToExit);
        this._userInterface.GetInput();
    }
}
