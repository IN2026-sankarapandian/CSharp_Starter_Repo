namespace UtilityApp.UserInput;

/// <summary>
/// Defines a contract for user interface.
/// </summary>
public interface IUserInput
{
    /// <summary>
    /// Gets user input and returns it.
    /// </summary>
    /// <returns>User's input</returns>
    public string? GetInput();
}