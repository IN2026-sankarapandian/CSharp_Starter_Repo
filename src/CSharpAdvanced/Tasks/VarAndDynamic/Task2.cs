using CSharpAdvanced.Constants;
using CSharpAdvanced.Enums;
using CSharpAdvanced.UserInterface;

namespace CSharpAdvanced.Tasks.VarAndDynamic;

/// <summary>
/// Demonstrates the working of var and dynamic.
/// </summary>
public class Task2 : ITask
{
    private readonly IUserInterface _userInterface;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task2"/> class.
    /// Creates an new instance of <see cref="Task2"/>.
    /// </summary>
    /// <param name="userInterface">Provides operations to interact with user.</param>
    public Task2(IUserInterface userInterface) => this._userInterface = userInterface;

    /// <inheritdoc/>
    public string Name => "Var and dynamic";

    /// <inheritdoc/>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);
        this._userInterface.ShowMessage(MessageType.Highlight, Messages.VarAndDynamicDescription);

        var varVariable = 123;

        this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.VarAndType, nameof(varVariable), varVariable, varVariable.GetType()));

        this._userInterface.ShowMessage(MessageType.Warning, string.Format(Messages.VarAndDynamicMessage1, nameof(varVariable)));

        // a = "hello"; //This will cause a error
        dynamic dynamicVariable = 123;
        this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.DynamicAndType, nameof(dynamicVariable), dynamicVariable, dynamicVariable.GetType()));
        this._userInterface.ShowMessage(MessageType.Information, Messages.VarAndDynamicMessage2);
        dynamicVariable = "abc";
        this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.DynamicAndType, nameof(dynamicVariable), dynamicVariable, dynamicVariable.GetType()));

        this._userInterface.ShowMessage(MessageType.Prompt, string.Format(Messages.TaskExitPrompt, 2));
        this._userInterface.GetInput();
    }
}
