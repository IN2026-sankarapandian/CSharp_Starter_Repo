namespace BoilerMachineApp.Enums;

/// <summary>
/// Specifies the type of message for displaying.
/// </summary>
public enum MessageType
{
    /// <summary>
    /// Represents the title shown to the user.
    /// </summary>
    Title,

    /// <summary>
    /// Represents the prompt message that requests user's input.
    /// </summary>
    Prompt,

    /// <summary>
    /// Represents the success message that's indicates success operation.
    /// </summary>
    Success,

    /// <summary>
    /// Represents the error message that's indicates error.
    /// </summary>
    Error,

    /// <summary>
    /// Represents the information message that's display info to user.
    /// </summary>
    Information,
}
