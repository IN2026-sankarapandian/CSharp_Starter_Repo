namespace UtilityApp.UserInput;

/// <summary>
/// Provides operations to interact with user via console.
/// </summary>
public class ConsoleInput : IUserInput
{
    /// <inheritdoc/>
    public string? GetInput()
    {
        return Console.ReadLine();
    }
}