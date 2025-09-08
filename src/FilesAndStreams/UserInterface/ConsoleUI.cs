using FilesAndStreams.Constants;
using FilesAndStreams.Enums;

namespace FilesAndStreams.UserInterface;

/// <summary>
/// Provide methods to interact with user via console UI.
/// </summary>
public class ConsoleUI : IUserInterface
{
    private readonly object _consoleLock = new object();
    private Dictionary<string, int> _taskLineMap;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleUI"/> class.
    /// </summary>
    public ConsoleUI()
    {
        this._taskLineMap = new Dictionary<string, int>();
    }

    /// <inheritdoc/>
    public string? GetInput()
    {
        lock (this._consoleLock)
        {
            return Console.ReadLine();
        }
    }

    /// <inheritdoc/>
    public void ShowMessage(MessageType type, string message)
    {
        lock (this._consoleLock)
        {
            message = message.Replace("\\n", Environment.NewLine);
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
    }

    /// <inheritdoc/>
    public void DrawProgressBar(string taskName, int progressPercentage, long elapsedTime = 0)
    {
        lock (this._consoleLock)
        {
            // Stores the position of the cursor before writing progress bar.
            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            // Checks whether the progress bar already exists.
            // If exists, map it with the existing one else create a new one.
            if (this._taskLineMap.ContainsKey(taskName))
            {
                int lineIndex = this._taskLineMap[taskName];
                Console.SetCursorPosition(0, lineIndex);
            }
            else
            {
                // If the new progress bar is created immediately after writing another,
                // then overlapping happen as the previous bar resets the position after writing.
                // So, this condition checks if the line is already used
                // if yes, move down to the next available line.
                while (this._taskLineMap.ContainsValue(top))
                {
                    top++;
                }

                this._taskLineMap[taskName] = top;
                Console.SetCursorPosition(0, top);
            }

            int total = 30;
            int currentProgress = progressPercentage * total / 100;
            string bar = new string(ProgressBarConstants.BarFilled, currentProgress).PadRight(total, ProgressBarConstants.BarEmpty);
            Console.WriteLine(ProgressBarConstants.ProgressBarTemplate, taskName, bar, progressPercentage, elapsedTime);

            if (progressPercentage == 100)
            {
                top++;
                this._taskLineMap.Remove(taskName);
            }

            // Resets the position after writing the progress bar
            Console.SetCursorPosition(left, top);
        }
    }
}
