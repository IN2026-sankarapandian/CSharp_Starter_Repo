using FilesAndStreams.Constants;
using FilesAndStreams.Enums;

namespace FilesAndStreams.UserInterface;

/// <summary>
/// Provide methods to interact with user via console UI.
/// </summary>
public class ConsoleUI
{
    /// <summary>
    /// Gets user input and returns it.
    /// </summary>
    /// <returns>User's input</returns>
    public string? GetInput()
    {
        return Console.ReadLine();
    }

    /// <summary>
    /// Shows the message to user as a specified type.
    /// </summary>
    /// <param name="type">Type of the message to show.</param>
    /// <param name="message">Message shown to user.</param>
    public void ShowMessage(MessageType type, string message)
    {
        switch (type)
        {
            case MessageType.Prompt:
                Console.Write(message);
                break;
            case MessageType.Information:
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(message);
                break;
            case MessageType.Title:
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(message);
                break;
        }

        Console.ResetColor();
    }

    /// <summary>
    /// Sets the position of the cursor.
    /// </summary>
    /// <param name="line">The line position of the cursor.</param>
    /// <param name="column">The column position of the cursor.</param>
    public void SetCursorPosition(int line, int column = 0) => Console.SetCursorPosition(column, line);

    /// <summary>
    /// Prints a progress bar with the name of the task, and time elapsed.
    /// </summary>
    /// <param name="taskName">Name of the task.</param>
    /// <param name="progressPercentage">Current progress in percentage</param>
    /// <param name="lineIndex">Index of the line at console.</param>
    /// <param name="elapsedTime">Elapsed time of the progress.</param>
    public void DrawProgressBar(string taskName, int progressPercentage, int lineIndex, long elapsedTime = 0)
    {
        int total = 30;
        int currentProgress = progressPercentage * total / 100;
        string bar = new string(ProgressBarConstants.BarFilled, currentProgress).PadRight(total, ProgressBarConstants.BarEmpty);
        Console.SetCursorPosition(0, lineIndex);
        Console.WriteLine(ProgressBarConstants.ProgressBarTemplate, taskName, bar, progressPercentage, elapsedTime);
    }
}
