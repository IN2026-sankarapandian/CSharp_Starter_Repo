using System.Diagnostics;
using System.Text;
using FilesAndStreams.Enums;
using FilesAndStreams.UserInterface;

namespace FilesAndStreams.Tasks;

/// <summary>
/// Demonstrates task 3.
/// </summary>
public class Task3
{
    private readonly ConsoleUI _consoleUI;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task3"/> class.
    /// </summary>
    /// <param name="consoleUI">Gives access to console UI</param>
    public Task3(ConsoleUI consoleUI)
    {
        this._consoleUI = consoleUI;
    }

    /// <summary>
    /// Its an optimized version of give code snippet.
    /// </summary>
    public void EfficientFileHandler()
    {
        this._consoleUI.ShowMessage(MessageType.Title, "Task 2");
        string path = "sample.text";
        string data = "This is Some test Data";
        Stopwatch stopwatch = new ();
        stopwatch.Start();
        using (FileStream fileStream = new (path, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 1024 * 16))
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            fileStream.Write(buffer, 0, buffer.Length);
        }

        using (BufferedStream fileStream = new (new FileStream(path, FileMode.Open)))
        {
            byte[] buffer = new byte[1024 * 16];
            int bytesRead;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                this._consoleUI.ShowMessage(MessageType.Information, Encoding.ASCII.GetString(buffer));
            }
        }

        this._consoleUI.ShowMessage(MessageType.Information, "Efficient file handle");
        stopwatch.Stop();
        TimeSpan elapsed = stopwatch.Elapsed;
        this._consoleUI.ShowMessage(MessageType.Information, $"Elapsed time: {elapsed.TotalMilliseconds} ms");

        this._consoleUI.ShowMessage(MessageType.Information, "Enter any key to exit task 3...");
        Console.ReadKey();
    }
}
