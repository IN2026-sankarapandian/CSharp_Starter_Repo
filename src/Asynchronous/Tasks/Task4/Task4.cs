using System.Text.Json;
using System.Text.RegularExpressions;
using Asynchronous.Common;
using Asynchronous.Constants;
using Asynchronous.Enums;
using Asynchronous.FileServices;
using Asynchronous.FormHandlers;
using Asynchronous.UserInterface;

namespace Asynchronous.Tasks.Task4;

/// <summary>
/// Demonstrates the task 4.
/// </summary>
public class Task4 : ITask
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandler _formHandler;
    private readonly FileService _fileService;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task4"/> class.
    /// </summary>
    /// <param name="userInterface">Provide method to interact with user via UI</param>
    /// <param name="formHandler">Provide methods to get data from the user.</param>
    /// <param name="fileService">Provide methods to handle files</param>
    public Task4(IUserInterface userInterface, FormHandler formHandler, FileService fileService)
    {
        this._userInterface = userInterface;
        this._formHandler = formHandler;
        this._fileService = fileService;
    }

    /// <inheritdoc/>
    public string Name => Messages.Task4Title;

    /// <inheritdoc/>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);

        this._userInterface.ShowMessage(MessageType.Highlight, Messages.Task4ToolDescription);
        string filePath = this._formHandler.GetTxtFilePath(Messages.EnterPath);

        try
        {
            Result<string> result = this.GetSummaryOfLongestWordFromFile(filePath).Result;
            if (result.IsSuccess)
            {
                this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.Summary, result.Value));
            }
            else
            {
                this._userInterface.ShowMessage(MessageType.Warning, result.ErrorMessage);
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
    /// Reads the file in specified path and return the longest word in it.
    /// </summary>
    /// <param name="path">Path of the file to read.</param>
    /// <returns>Longest word in the file.</returns>
    public async Task<Result<string>> GetLongestWordFromFile(string path)
    {
        Result<string> essayResult = await this._fileService.ReadFileAsync(path, (progress, elapsedTime) =>
        {
            this._userInterface.DrawProgressBar(Messages.ReadingFile, progress, elapsedTime);
        });

        if (!essayResult.IsSuccess)
        {
            return Result<string>.Failure(essayResult.ErrorMessage);
        }

        // Clean text and get longest word
        string essay = Regex.Replace(essayResult.Value.ToLower(), RegexConstants.WordFilter, string.Empty);
        string[] words = essay.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string longestWord = words.OrderByDescending(word => word.Length).FirstOrDefault() ?? string.Empty;

        return Result<string>.Success(longestWord);
    }

    /// <summary>
    /// Gets the data of longest word in the specified file.
    /// </summary>
    /// <param name="path">Path of the file to read.</param>
    /// <returns>Data about the longest word in the file.</returns>
    public async Task<Result<string>> GetDataOfLongestWordFromFile(string path) // Method B
    {
        Result<string> wordResult = await this.GetLongestWordFromFile(path);

        if (!wordResult.IsSuccess)
        {
            return Result<String>.Failure(wordResult.ErrorMessage);
        }

        string url = $"https://en.wikipedia.org/api/rest_v1/page/summary/{wordResult.Value}";

        using HttpClient client = new ();
        client.DefaultRequestHeaders.Add("User-Agent", "App");
        string data = await client.GetStringAsync(url);

        return Result<string>.Success(data);
    }

    /// <summary>
    /// Gets the summary of the longest word in the specified file.
    /// </summary>
    /// <param name="path">Path of the file to read.</param>
    /// <returns>Summary of the longest word in the file.</returns>
    public async Task<Result<string>> GetSummaryOfLongestWordFromFile(string path) // Method C
    {
        Result<string> jsonResult = await this.GetDataOfLongestWordFromFile(path);

        if (!jsonResult.IsSuccess)
        {
            return Result<string>.Failure(jsonResult.ErrorMessage);
        }

        using JsonDocument doc = JsonDocument.Parse(jsonResult.Value);
        string? summary = null;
        if (doc.RootElement.TryGetProperty("extract", out JsonElement extract))
        {
            summary = extract.GetString();
        }

        return Result<string>.Success(summary ?? Messages.NoSummary);
    }
}
