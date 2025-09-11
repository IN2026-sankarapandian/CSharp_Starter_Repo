using FilesAndStreams.Constants;
using FilesAndStreams.Enums;
using FilesAndStreams.UserInterface.Components;

namespace FilesAndStreams.UserInterface;

/// <summary>
/// Provide methods to interact with user via console UI.
/// </summary>
public class ConsoleUI : IUserInterface
{
    private readonly object _consoleLock = new ();
    private readonly List<ProgressBar> _progressBars;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleUI"/> class.
    /// </summary>
    public ConsoleUI()
    {
        this._progressBars = new List<ProgressBar>();
    }

    /// <inheritdoc/>
    public string? GetInput()
    {
        return Console.ReadLine();
    }

    /// <inheritdoc/>
    public void ShowMessage(MessageType type, string message)
    {
        lock (this._consoleLock)
        {
            // Stores the position of the cursor before writing progress bar.
            int top = Console.CursorTop;

            // If the new console log is created immediately after writing a progress bar,
            // then overlapping happen as the previous bar resets the position after writing.
            // So, this condition checks if the line is already used
            // if yes, move down to the next available line.
            while (this._progressBars.Any(progressBar => progressBar.LineIndex == top))
            {
                top++;
            }

            Console.SetCursorPosition(0, top);

            message = message.Replace("\\n", Environment.NewLine);
            switch (type)
            {
                case MessageType.Prompt:
                    Console.Write(message);
                    break;
                case MessageType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(message);
                    break;
                case MessageType.Title:
                    Console.Clear();
                    this.ResetProgressBars();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(message);
                    break;
                case MessageType.Information:
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
            ProgressBar? progressBar = this._progressBars.Where(progressBar => progressBar.TaskName == taskName).FirstOrDefault<ProgressBar>();

            // Checks whether the progress bar already exists.
            // If exists, map it with the existing one else create a new one.
            if (progressBar is not null)
            {
                progressBar.Progress = progressPercentage;
                progressBar.ElapsedTime = elapsedTime;
                Console.SetCursorPosition(0, progressBar.LineIndex);
            }
            else
            {
                // If the new progress bar is created immediately after writing another,
                // then overlapping happen as the previous bar resets the position after writing.
                // So, this condition checks if the line is already used
                // if yes, move down to the next available line.
                while (this._progressBars.Any(progressBar => progressBar.LineIndex == top))
                {
                    top++;
                }

                progressBar = new ProgressBar
                {
                    TaskName = taskName,
                    Progress = progressPercentage,
                    ElapsedTime = elapsedTime,
                    IsStatic = false,
                    LineIndex = top,
                };

                this._progressBars.Add(progressBar);
                Console.SetCursorPosition(0, top);
            }

            Console.WriteLine(progressBar.ToString());

            if (progressPercentage == 100)
            {
                if (!progressBar.IsStatic)
                {
                    top++;
                }

                this._progressBars.Remove(progressBar);
            }

            // Resets the position after writing the progress bar
            Console.SetCursorPosition(left, top);
        }
    }

    /// <summary>
    /// Resets running progress bar to top of the page when page changes.
    /// </summary>
    private void ResetProgressBars()
    {
        int line = 0;
        foreach (ProgressBar progressBar in this._progressBars)
        {
            progressBar.LineIndex = line;
            progressBar.IsStatic = true;
            this.DrawProgressBar(progressBar.TaskName ?? Messages.Unknown, progressBar.Progress, progressBar.ElapsedTime);
            line++;
        }

        Console.Write(new string('\n', this._progressBars.Count));
    }
}
