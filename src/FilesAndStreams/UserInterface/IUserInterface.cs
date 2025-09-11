using FilesAndStreams.Enums;

namespace FilesAndStreams.UserInterface;

/// <summary>
/// Defines a contract for user interface.
/// </summary>
public interface IUserInterface
{
    /// <summary>
    /// Gets user input and returns it.
    /// </summary>
    /// <returns>User's input</returns>
    public string? GetInput();

    /// <summary>
    /// Shows the message to user as a specified type.
    /// </summary>
    /// <param name="type">Type of the message to show.</param>
    /// <param name="message">Message shown to user.</param>
    public void ShowMessage(MessageType type, string message);

    /// <summary>
    /// Prints a progress bar with the name of the task, and time elapsed.
    /// </summary>
    /// <param name="taskName">Unique name of the task.(Used to map with the progress bar component)</param>
    /// <param name="progressPercentage">Current progress in percentage</param>
    /// <param name="elapsedTime">Elapsed time of the progress.</param>
    public void DrawProgressBar(string taskName, int progressPercentage, long elapsedTime = 0);
}
