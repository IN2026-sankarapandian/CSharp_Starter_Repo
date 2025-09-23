using Asynchronous.Constants;

namespace Asynchronous.UserInterface.Components;

/// <summary>
/// Represents the progress bar UI component.
/// </summary>
public class ProgressBar
{
    /// <summary>
    /// Gets or sets name of the task having this progress bar.
    /// </summary>
    /// <value>
    /// Name of the task having this progress bar.
    /// </value>
    public string? TaskName { get; set; }

    /// <summary>
    /// Gets or sets the current progress in percentage.
    /// </summary>
    /// <value>
    /// The current progress in percentage.
    /// </value>
    public int Progress { get; set; }

    /// <summary>
    /// Gets or sets the current elapsed time by the process.
    /// </summary>
    /// <value>
    /// The current elapsed time by the process.
    /// </value>
    public long ElapsedTime { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether state of progress bar is static or not.
    /// </summary>
    /// <value>
    /// The state of the progress bar in UI.
    /// </value>
    public bool IsStatic { get; set; }

    /// <summary>
    /// Gets or sets the console line reserved for this progress bar.
    /// </summary>
    /// <value>
    /// The console line reserved for this progress bar.
    /// </value>
    public int LineIndex { get; set; }

    /// <summary>
    /// Returns the progress bar as string.
    /// </summary>
    /// <returns>Progress bar as string.</returns>
    public override string ToString()
    {
        int total = 30;
        int currentProgress = this.Progress * total / 100;

        string bar = new string(ProgressBarConstants.BarFilled, currentProgress).PadRight(total, ProgressBarConstants.BarEmpty);
        return string.Format(ProgressBarConstants.ProgressBarTemplate, this.TaskName, bar, this.Progress, this.ElapsedTime);
    }
}
