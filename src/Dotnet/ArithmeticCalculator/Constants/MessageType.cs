namespace Dotnet.ArithmeticCalculator.Constants;

/// <summary>
/// Specifies the type of message for displaying.
/// </summary>
public enum MessageType
{
    /// <summary>
    /// Represents the warning message that's indicates some issues.
    /// </summary>
    Warning = 0,

    /// <summary>
    /// Represents the result to shown to the user after an operation.
    /// </summary>
    Result,

    /// <summary>
    /// Represents the prompt message that requests user's input.
    /// </summary>
    Prompt,

    /// <summary>
    /// Represents the title shown to the user.
    /// </summary>
    Title,
}
