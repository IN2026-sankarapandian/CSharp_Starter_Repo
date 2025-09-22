using System.Diagnostics;
using System.Text;
using FilesAndStreams.Constants;
using FilesAndStreams.Enums;
using FilesAndStreams.UserInterface;

namespace FilesAndStreams.Tasks;

/// <summary>
/// Demonstrates task 4.
/// </summary>
public class Task4
{
    private static readonly object _lock = new ();
    private readonly IUserInterface _userInterface;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task4"/> class.
    /// </summary>
    /// <param name="userInterface">Gives access to UI</param>
    public Task4(IUserInterface userInterface)
    {
        this._userInterface = userInterface;
    }

    /// <summary>
    /// Its an entry point for <see cref="Task4"/>
    /// </summary>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.TaskTitle, 4));

        this.SimulateLoggingErrors();
        this._userInterface.ShowMessage(MessageType.Information, Messages.ErrorLogFilesSimulated);

        this._userInterface.ShowMessage(MessageType.Prompt, Messages.PressEnterToExit);
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Simulate multiple users logging errors at the same time.
    /// </summary>
    private void SimulateLoggingErrors()
    {
        Task loggingErrorForUser1 = Task.Run(() => this.LogError(FileResources.SampleErrorData, FileResources.SampleUserID1));
        Task loggingErrorForUser2 = Task.Run(() => this.LogError(FileResources.SampleErrorData, FileResources.SampleUserID2));
        Task loggingErrorForUser3 = Task.Run(() => this.LogError(FileResources.SampleErrorData, FileResources.SampleUserID3));
        Task.WaitAll(loggingErrorForUser1, loggingErrorForUser2, loggingErrorForUser3);
    }

    /// <summary>
    /// Log the error in separate text file.
    /// </summary>
    /// <param name="message">Error message to write.</param>
    /// <param name="userID">Id of the user.</param>
    private void LogError(string message, string userID)
    {
        Stopwatch watch1 = Stopwatch.StartNew();
        lock (_lock)
        {
            string errorFilePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                string.Format(FileResources.ErrorLogFileName, userID));
            using FileStream fileStream = new (errorFilePath, FileMode.Append);
            byte[] error = Encoding.UTF8.GetBytes(message);
            fileStream.Write(error, 0, error.Length);
        }

        watch1.Stop();
    }
}
