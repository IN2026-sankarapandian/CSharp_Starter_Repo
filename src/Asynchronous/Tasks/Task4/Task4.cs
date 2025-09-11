using System.Text.Json;
using System.Text.RegularExpressions;
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
            string result = this.GetSummaryOfLongestWordFromFile(filePath).Result;
            this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.Summary, result));
        }
        catch (Exception ex)
        {
            this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.Error, ex.Message));
        }

        this._userInterface.ShowMessage(MessageType.Information, Messages.EnterToExit);
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Reads the file in specified path and return the longest word in it.
    /// </summary>
    /// <param name="path">Path of the file to read.</param>
    /// <returns>Longest word in the file.</returns>
    public Task<string> GetLongestWordFromFile(string path) // Method A
    {
        return Task<string>.Run(() =>
        {
            string essay = this._fileService.ReadFileAsync(path, (progress, elapsedTime) =>
            {
                this._userInterface.DrawProgressBar(Messages.ReadingFile, progress, elapsedTime);
            }).Result;

            // Removes non word, non space characters
            essay = essay.ToLower();
            essay = Regex.Replace(essay, RegexConstants.WordFilter, string.Empty);

            string[] words = essay.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return words.OrderByDescending(word => word.Length).FirstOrDefault() ?? string.Empty;
        });
    }

    /// <summary>
    /// Gets the data of longest word in the specified file.
    /// </summary>
    /// <param name="path">Path of the file to read.</param>
    /// <returns>Data about the longest word in the file.</returns>
    public async Task<string> GetDataOfLongestWordFromFile(string path) // Method B
    {
        string word = await this.GetLongestWordFromFile(path);
        string url = $"https://en.wikipedia.org/api/rest_v1/page/summary/{word}";

        using HttpClient client = new ();
        client.DefaultRequestHeaders.Add("User-Agent", "App");
        string data = await client.GetStringAsync(url);

        return data;
    }

    /// <summary>
    /// Gets the summary of the longest word in the specified file.
    /// </summary>
    /// <param name="path">Path of the file to read.</param>
    /// <returns>Summary of the longest word in the file.</returns>
    public async Task<string> GetSummaryOfLongestWordFromFile(string path) // Method C
    {
        string json = await this.GetDataOfLongestWordFromFile(path);
        using JsonDocument doc = JsonDocument.Parse(json);
        string? summary = null;
        if (doc.RootElement.TryGetProperty("extract", out JsonElement extract))
        {
            summary = extract.GetString();
        }

        return summary ?? Messages.NoSummary;
    }
}
