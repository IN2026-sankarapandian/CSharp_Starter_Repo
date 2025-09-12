using CSharpAdvanced.Constants;
using CSharpAdvanced.Enums;
using CSharpAdvanced.UserInterface;

namespace CSharpAdvanced.Tasks.AnonymousMethod;

/// <summary>
/// Demonstrates the usage and working of anonymous method.
/// </summary>
public class Task3 : ITask
{
    private readonly IUserInterface _userInterface;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task3"/> class.
    /// </summary>
    /// <param name="userInterface">Provides operations to interact with user.</param>
    public Task3(IUserInterface userInterface) => this._userInterface = userInterface;

    /// <inheritdoc/>
    public string Name => Titles.TaskTitle3;

    /// <inheritdoc/>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);
        this._userInterface.ShowMessage(MessageType.Highlight, Messages.AnonymousDescription);

        int[] integers = { 1, 2, 3, 6, 5, 4, 10 };

        this._userInterface.ShowMessage(MessageType.Information, Messages.ArrayBeforeSorting);
        this.PrintIntegers(integers);

        // Creating a comparison with anonymous method and sorting the array with it.
        Comparison<int> comparison = (integerA, integerB) =>
        {
            return integerA.CompareTo(integerB);
        };
        Array.Sort(integers, comparison);

        this._userInterface.ShowMessage(MessageType.Information, Messages.ArrayAfterSorting);
        this.PrintIntegers(integers);

        this._userInterface.ShowMessage(MessageType.Prompt, string.Format(Messages.TaskExitPrompt, 3));
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Displays the integers list to the user.
    /// </summary>
    /// <param name="integers">Integers list to display.</param>
    private void PrintIntegers(int[] integers)
    {
        foreach (int integer in integers)
        {
            this._userInterface.ShowMessage(MessageType.Information, integer + " ");
        }
    }
}
