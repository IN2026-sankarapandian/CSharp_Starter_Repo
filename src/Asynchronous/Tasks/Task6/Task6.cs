using System.Text.RegularExpressions;
using Asynchronous.Common;
using Asynchronous.Constants;
using Asynchronous.Enums;
using Asynchronous.FileServices;
using Asynchronous.FormHandlers;
using Asynchronous.UserInterface;

namespace Asynchronous.Tasks.Task6;

/// <summary>
/// Demonstrates task 6.
/// </summary>
public class Task6 : ITask
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandler _formHandler;
    private readonly FileService _fileService;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task6"/> class.
    /// </summary>
    /// <param name="userInterface">Provide method to interact with user via UI</param>
    /// <param name="formHandler">Provide methods to get data from the user.</param>
    /// <param name="fileService">Provide methods to handle files</param>
    public Task6(IUserInterface userInterface, FormHandler formHandler, FileService fileService)
    {
        this._userInterface = userInterface;
        this._formHandler = formHandler;
        this._fileService = fileService;
    }

    /// <inheritdoc/>
    public string Name => Messages.Task6Title;

    /// <inheritdoc/>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);
        try
        {
            this._userInterface.ShowMessage(MessageType.Highlight, Messages.Task6ToolDescription);
            string filePath = this._formHandler.GetTxtFilePath(Messages.EnterPath);
            Result<int> totalNumberOfWordsResult = this.GetTotalNumberOfWordsInFile(filePath).Result;

            if (totalNumberOfWordsResult.IsSuccess)
            {
                this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.TotalWords, totalNumberOfWordsResult.Value));
            }
            else
            {
                this._userInterface.ShowMessage(MessageType.Warning, totalNumberOfWordsResult.ErrorMessage);
            }
        }
        catch (Exception ex)
        {
            this._userInterface.ShowMessage(MessageType.Warning, string.Format(Messages.CantReadFile, ex.Message));
        }

        this._userInterface.ShowMessage(MessageType.Information, Messages.EnterToExit);
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Gets the total number of words in the text file.
    /// </summary>
    /// <param name="path">Path of the file to get its total number of words.</param>
    /// <returns>Total number of words in the specified file.</returns>
    private async Task<Result<int>> GetTotalNumberOfWordsInFile(string path) // Method B
    {
        this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.ThreadUsedBeforeAwait, Thread.CurrentThread.ManagedThreadId));

        Result<string> contentResult = await this._fileService.ReadFileAsync(
            path,
            (progress, elapsedTime)
            => this._userInterface.DrawProgressBar(Messages.ReadingFile, progress, elapsedTime))
            .ConfigureAwait(false); // Method A

        this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.ThreadUsedAfterAwait, Thread.CurrentThread.ManagedThreadId));
        if (!contentResult.IsSuccess)
        {
            return Result<int>.Failure(contentResult.ErrorMessage);
        }

        // Removes non word, non space characters
        string content = Regex.Replace(contentResult.Value, RegexConstants.WordFilter, string.Empty);
        string[] words = content.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return Result<int>.Success(words.Length);
    }
}
