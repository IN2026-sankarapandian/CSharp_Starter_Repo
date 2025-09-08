using System.Diagnostics;
using System.Text;
using FilesAndStreams.Constants;
using FilesAndStreams.Enums;
using FilesAndStreams.UserInterface;

namespace FilesAndStreams.Tasks;

/// <summary>
/// Demonstrates task 3.
/// </summary>
public class Task3
{
    private readonly IUserInterface _userInterface;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task3"/> class.
    /// </summary>
    /// <param name="userInterface">Gives access to UI</param>
    public Task3(IUserInterface userInterface)
    {
        this._userInterface = userInterface;
    }

    /// <summary>
    /// Its an entry point for <see cref="Task4"/>
    /// </summary>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.TaskTitle, 3));
        this.EfficientFileHandler();
        this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.PressEnterToExitTask, 3));
        Console.ReadKey();
    }

    /// <summary>
    /// Its an optimized version of give code snippet.
    /// </summary>
    private void EfficientFileHandler()
    {
        string path = FileResources.SampleFilePath;
        string data = FileResources.SampleData;
        Stopwatch stopwatch = new ();
        stopwatch.Start();
        using (FileStream fileStream = new (
            path,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            bufferSize: 1024 * 16))
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
                this._userInterface.ShowMessage(MessageType.Information, Encoding.ASCII.GetString(buffer));
            }
        }

        this._userInterface.ShowMessage(MessageType.Information, Messages.EfficientFilehandle);
        stopwatch.Stop();
        TimeSpan elapsed = stopwatch.Elapsed;
        this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.Elapsedtime, elapsed.TotalMilliseconds));
    }
}
