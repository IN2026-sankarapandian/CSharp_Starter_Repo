namespace Asynchronous.Constants;

/// <summary>
/// Have constants related to the progress bar component.
/// </summary>
public static class ProgressBarConstants
{
    /// <summary>
    /// Denotes that bar is filled
    /// </summary>
    public const char BarFilled = '|';

    /// <summary>
    /// Denotes that bar is empty.
    /// </summary>
    public const char BarEmpty = '-';

    /// <summary>
    /// Denotes the template of the progress bar
    /// </summary>
    public const string ProgressBarTemplate = "{0} : [{1}] {2}%, time : {3}ms";
}
