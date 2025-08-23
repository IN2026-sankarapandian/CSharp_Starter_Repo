namespace UtilityApp.UserInput;

/// <summary>
/// Provides operations to get user input via console..
/// </summary>
public class ConsoleInput : IUserInput
{
    /// <inheritdoc/>
    public string? GetInput()
    {
        return Console.ReadLine();
    }
}