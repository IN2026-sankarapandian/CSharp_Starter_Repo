using System.Diagnostics;
using System.Text;
using FilesAndStreams.Enums;
using FilesAndStreams.UserInterface;

namespace FilesAndStreams.Tasks;

/// <summary>
/// Demonstrates task 4.
/// </summary>
public class Task4
{
    private static readonly object _lock = new ();
    private readonly ConsoleUI _consoleUI;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task4"/> class.
    /// </summary>
    /// <param name="consoleUI">Gives access to console UI</param>
    public Task4(ConsoleUI consoleUI)
    {
        this._consoleUI = consoleUI;
    }

    /// <summary>
    /// Its an entry point for <see cref="Task4"/>
    /// </summary>
    public void Run()
    {
        this._consoleUI.ShowMessage(MessageType.Title, "Task 2");
        Task loggingErrorForUser1 = Task.Run(() => this.LogError("Error a", "001"));
        Task loggingErrorForUser2 = Task.Run(() => this.LogError("Error b", "002"));
        Task loggingErrorForUser3 = Task.Run(() => this.LogError("Error c", "003"));
        Task.WaitAll(loggingErrorForUser1, loggingErrorForUser2, loggingErrorForUser3);
        this._consoleUI.ShowMessage(MessageType.Information, "Enter any key to exit task 4...");
        Console.ReadKey();
    }

    /// <summary>
    /// Log the error in separate text file.
    /// </summary>
    /// <param name="message">Error message to write.</param>
    /// <param name="userID">Id of the user.</param>
    public void LogError(string message, string userID)
    {
        Stopwatch watch1 = Stopwatch.StartNew();
        lock (_lock)
        {
            string errorFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{userID}-ErrorLog.txt");
            using FileStream fileStream = new (errorFilePath, FileMode.Append);
            byte[] error = Encoding.UTF8.GetBytes(message);
            fileStream.Write(error, 0, error.Length);
        }

        watch1.Stop();
        this._consoleUI.ShowMessage(MessageType.Information, $"Time taken by provided code {watch1.ElapsedMilliseconds}");
    }
}
