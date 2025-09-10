using System.Diagnostics;
using System.Text;
using FilesAndStreams.Constants;
using FilesAndStreams.Enums;
using FilesAndStreams.FormHandlers;
using FilesAndStreams.UserInterface;

namespace FilesAndStreams.Tasks;

/// <summary>
/// Demonstrates task 3.
/// </summary>
public class Task3
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandler _formHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task3"/> class.
    /// </summary>
    /// <param name="userInterface">Gives access to UI</param>
    /// <param name="formHandler">Get data from user.</param>
    public Task3(IUserInterface userInterface, FormHandler formHandler)
    {
        this._userInterface = userInterface;
        this._formHandler = formHandler;
    }

    /// <summary>
    /// Its an entry point for <see cref="Task3"/>
    /// </summary>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.TaskTitle, 3));

        string sampleFileSavePath = this._formHandler.GetTxtFileSavePath(Messages.EnterPathToSaveFile);
        string sampleData = this._formHandler.GetUserInput(Messages.EnterValue);

        try
        {
            this.EfficientFileHandler(sampleFileSavePath, sampleData);
        }
        catch (IOException ex)
        {
            this._userInterface.ShowMessage(MessageType.Warning, string.Format(Messages.PromptErrorAndGoBack, ex.Message));
        }

        this._userInterface.ShowMessage(MessageType.Information, Messages.PressEnterToExit);
        Console.ReadKey();
    }

    /// <summary>
    /// Its an optimized version of give code snippet.
    /// </summary>
    private void EfficientFileHandler(string path, string data)
    {
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

        stopwatch.Stop();
        TimeSpan elapsed = stopwatch.Elapsed;
        this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.ElapsedTime, elapsed.TotalMilliseconds));
    }
}
