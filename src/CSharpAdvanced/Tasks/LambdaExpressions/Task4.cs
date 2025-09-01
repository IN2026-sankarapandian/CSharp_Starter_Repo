using CSharpAdvanced.Constants;
using CSharpAdvanced.Enums;
using CSharpAdvanced.UserInterface;

namespace CSharpAdvanced.Tasks.LambdaExpressions;

/// <summary>
/// Demonstrates the usage and working of anonymous method.
/// </summary>
public class Task4 : ITask
{
    private readonly IUserInterface _userInterface;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task4"/> class.
    /// </summary>
    /// <param name="userInterface">Provides operations to interact with user.</param>
    public Task4(IUserInterface userInterface) => this._userInterface = userInterface;

    /// <inheritdoc/>
    public string Name => "Lambda expressions";

    /// <inheritdoc/>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);
        this._userInterface.ShowMessage(MessageType.Highlight, Messages.LambdaExpressionDescription);

        int[] integers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int[] squaredEvenIntegers = integers.Where(integer => integer % 2 != 0).Select(integer => integer * integer).ToArray();

        this._userInterface.ShowMessage(MessageType.Information, Messages.SquaredOddNumbers);
        foreach (int integer in squaredEvenIntegers)
        {
            this._userInterface.ShowMessage(MessageType.Information, integer + " ");
        }

        this._userInterface.ShowMessage(MessageType.Prompt, string.Format(Messages.TaskExitPrompt, 4));
        this._userInterface.GetInput();
    }
}
