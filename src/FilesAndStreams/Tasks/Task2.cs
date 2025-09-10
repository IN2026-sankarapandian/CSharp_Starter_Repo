using FilesAndStreams.Constants;
using FilesAndStreams.Enums;
using FilesAndStreams.FileServices;
using FilesAndStreams.FormHandlers;
using FilesAndStreams.UserInterface;

namespace FilesAndStreams.Tasks;

/// <summary>
/// Demonstrates the how different streams work in asynchronous mode.
/// </summary>
public class Task2
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandler _formHandler;
    private readonly FileService _fileService;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task2"/> class.
    /// </summary>
    /// <param name="userInterface">Gives access to UI</param>
    /// <param name="formHandler">Get data from user.</param>
    /// <param name="fileService">Provide methods to handle files</param>
    public Task2(IUserInterface userInterface, FormHandler formHandler, FileService fileService)
    {
        this._userInterface = userInterface;
        this._formHandler = formHandler;
        this._fileService = fileService;
    }

    /// <summary>
    /// Its an entry point for <see cref="Task2"/>
    /// </summary>
    public void Run() => this.HandleMenu();

    /// <summary>
    /// Handles the menu for task 2.
    /// </summary>
    private void HandleMenu()
    {
        do
        {
            this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.TaskTitle, 2));
            this._userInterface.ShowMessage(MessageType.Information, Messages.Task2MenuPrompt);
            string userChoice = this._formHandler.GetUserInput("Enter what do you want to do : ");
            switch (userChoice)
            {
                case "1":
                    _ = this.HandleCreateSampleFilesAsync();
                    break;
                case "2":
                    _ = this.HandleReadSampleFilesAsync();
                    break;
                case "3":
                    this.HandleCreateFilteredFileAsync();
                    break;
                case "4":
                    return;
                default:
                    this._userInterface.ShowMessage(MessageType.Information, Messages.EnterValidOption);
                    break;
            }
        }
        while (true);
    }

    /// <summary>
    /// Handles creating sample files needed for the task.
    /// </summary>
    private async Task HandleCreateSampleFilesAsync()
    {
        try
        {
            this._userInterface.ShowMessage(MessageType.Title, Messages.CreateAsync);
            string sampleFileSavePath = this._formHandler.GetTxtFileSavePath(Messages.EnterPathToSaveFile);
            string taskName = string.Format(Messages.WritingFile, Path.GetFileName(sampleFileSavePath));
            this._userInterface.DrawProgressBar(taskName, 0, 0);
            await this._fileService.CreateLargeTextFileAsync(sampleFileSavePath, FileResources.TargetSize, (progress, elapsedTime)
                => this._userInterface.DrawProgressBar(taskName, progress, elapsedTime));
        }
        catch (IOException ex)
        {
            this._userInterface.ShowMessage(MessageType.Warning, string.Format(Messages.PromptErrorAndGoBack, ex.Message));
            this._userInterface.GetInput();
        }
    }

    /// <summary>
    /// Handles reading sample files with different streams
    /// </summary>
    private async Task HandleReadSampleFilesAsync()
    {
        try
        {
            this._userInterface.ShowMessage(MessageType.Title, Messages.ReadAsync);
            string sampleFileSavePath = this._formHandler.GetTxtFilePath(Messages.EnterPathToReadFile);
            string taskNameForFileStream = string.Format(Messages.ReadingFileWithFileStream, Path.GetFileName(sampleFileSavePath));
            string taskNameForBufferedStream = string.Format(Messages.ReadingFileWithBufferedStream, Path.GetFileName(sampleFileSavePath));
            this._userInterface.ShowMessage(MessageType.Information, Messages.FileReadOptions);
            string userChoice = this._formHandler.GetUserInput(Messages.EnterWhichStreamToUse);

            switch (userChoice)
            {
                case "1":
                    this._userInterface.DrawProgressBar(taskNameForFileStream, 0, 0);
                    await Task.Run(() => this._fileService.ReadFileInChunksAsync(FileReader.FileStream, sampleFileSavePath, (progress, elapsedTime)
                        => this._userInterface.DrawProgressBar(taskNameForFileStream, progress, elapsedTime)));
                    break;
                case "2":
                    this._userInterface.DrawProgressBar(taskNameForBufferedStream, 0, 0);
                    await Task.Run(() => this._fileService.ReadFileInChunksAsync(FileReader.FileStream, sampleFileSavePath, (progress, elapsedTime)
                        => this._userInterface.DrawProgressBar(taskNameForBufferedStream, progress, elapsedTime)));
                    break;
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

    /// <summary>
    /// Handles processing data.
    /// </summary>
    private async void HandleCreateFilteredFileAsync()
    {
        try
        {
            this._userInterface.ShowMessage(MessageType.Title, Messages.ProcessAsync);
            string sampleFilePath = this._formHandler.GetTxtFilePath(Messages.EnterPathToFilterFile);
            string filterFilePath = this._formHandler.GetTxtFileSavePath(Messages.EnterPathToSaveFilteredFile);
            decimal temperatureThreshold = this._formHandler.GetTemperatureThreshold(Messages.EnterTemperatureThresholdValue);
            string readingTaskName = string.Format(Messages.ReadingFileForFiltering, Path.GetFileName(sampleFilePath));
            string filteringTaskName = string.Format(Messages.FilteringFile, Path.GetFileName(sampleFilePath));
            string writingTaskName = string.Format(Messages.WritingFilteredFile, Path.GetFileName(filterFilePath));
            this._userInterface.DrawProgressBar(readingTaskName, 0, 0);
            this._userInterface.DrawProgressBar(filteringTaskName, 0, 0);
            this._userInterface.DrawProgressBar(writingTaskName, 0, 0);
            string content = await this._fileService.ReadFileAsync(sampleFilePath, (progress, elapsedTime)
                => this._userInterface.DrawProgressBar(readingTaskName, progress, elapsedTime));
            string filteredContent = this._fileService.FilterByTemperature(content, 100, (progress, elapsedTime)
                => this._userInterface.DrawProgressBar(filteringTaskName, progress, elapsedTime));
            await this._fileService.WriteDataAsync(filterFilePath, filteredContent, (progress, elapsedTime)
                => this._userInterface.DrawProgressBar(writingTaskName, progress, elapsedTime));
        }
        catch (IOException ex)
        {
            this._userInterface.ShowMessage(MessageType.Warning, string.Format(Messages.PromptErrorAndGoBack, ex.Message));
            this._userInterface.GetInput();
        }
    }
}