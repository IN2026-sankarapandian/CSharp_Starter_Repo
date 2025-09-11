using Asynchronous.Common;
using Asynchronous.Constants;
using Asynchronous.Enums;
using Asynchronous.FormHandlers;
using Asynchronous.UserInterface;

namespace Asynchronous.Tasks.Task1;

/// <summary>
/// Demonstrates the task 1
/// </summary>
public class Task1 : ITask
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandler _formHandler;
    private readonly HttpContentFetcher _downloader;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task1"/> class.
    /// </summary>
    /// <param name="downloader">Provide methods to download content from online.</param>
    /// <param name="formHandler">Provide methods to get data from the user.</param>
    /// <param name="userInterface">Provide method to interact with user via UI</param>
    public Task1(IUserInterface userInterface, FormHandler formHandler, HttpContentFetcher downloader)
    {
        this._userInterface = userInterface;
        this._formHandler = formHandler;
        this._downloader = downloader;
    }

    /// <inheritdoc/>
    public string Name => Messages.Task1Title;

    /// <inheritdoc/>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);

        this._userInterface.ShowMessage(MessageType.Information, Messages.ExampleURL);
        string userInputUrl = this._formHandler.GetUrl(Messages.EnterURL);

        this.HandleFetchData(userInputUrl);

        this._userInterface.ShowMessage(MessageType.Information, Messages.EnterToExit);
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Fetch the data from url and prints it.
    /// </summary>
    /// <param name="userInputUrl">URL to fetch from.</param>
    private void HandleFetchData(string userInputUrl)
    {
        this._userInterface.ShowMessage(MessageType.Information, Messages.FetchingData);
        Result<string> contentResult = this._downloader.DownloadContentAsync(userInputUrl).Result;

        if (contentResult.IsSuccess)
        {
            this._userInterface.ShowMessage(MessageType.Information, contentResult.Value);
        }
        else
        {
            this._userInterface.ShowMessage(MessageType.Information, contentResult.ErrorMessage);
        }
    }
}
