using Asynchronous.Constants;
using Asynchronous.Enums;
using Asynchronous.UserInterface;

namespace Asynchronous.Tasks.Task7;

/// <summary>
/// Demonstrates task 7.
/// </summary>
public class Task7 : ITask
{
    private readonly IUserInterface _userInterface;
    private readonly ExceptionThrower _exceptionThrower;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task7"/> class.
    /// </summary>
    /// <param name="userInterface">Provide method to interact with user via UI</param>
    /// <param name="exceptionThrower">Provide methods that throw exceptions</param>
    public Task7(IUserInterface userInterface, ExceptionThrower exceptionThrower)
    {
        this._userInterface = userInterface;
        this._exceptionThrower = exceptionThrower;
    }

    /// <inheritdoc/>
    public string Name => Messages.Task7Title;

    /// <inheritdoc/>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);

        try
        {
            this._exceptionThrower.TaskAsyncExceptionThrower().Wait();
        }
        catch (Exception ex)
        {
            this._userInterface.ShowMessage(MessageType.Information, ex.Message);
        }

        try
        {
            this._exceptionThrower.VoidAsyncExceptionThrower();
        }
        catch (Exception ex)
        {
            this._userInterface.ShowMessage(MessageType.Information, ex.Message);
        }

        this._userInterface.ShowMessage(MessageType.Information, Messages.EnterToExit);
        this._userInterface.GetInput();
    }
}
