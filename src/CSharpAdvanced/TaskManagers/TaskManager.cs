using CSharpAdvanced.Constants;
using CSharpAdvanced.Enums;
using CSharpAdvanced.Tasks;
using CSharpAdvanced.UserInterface;

namespace CSharpAdvanced.TaskManagers;

/// <summary>
/// Handles the tasks of assignment <see cref="CSharpAdvanced"/>.
/// </summary>
public class TaskManager
{
    private readonly IUserInterface _userInterface;
    private readonly List<ITask> _tasks;

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskManager"/> class.
    /// </summary>
    /// <param name="userInterface">Gives access to the user interface.</param>
    /// <param name="tasks">Tasks to run by this task manager.</param>
    public TaskManager(IUserInterface userInterface, IEnumerable<ITask> tasks)
    {
        this._userInterface = userInterface;
        this._tasks = tasks.ToList();
    }

    /// <summary>
    /// Handle and show menu page to the user.
    /// </summary>
    public void HandleMenu()
    {
        while (true)
        {
            this._userInterface.ShowMessage(MessageType.Title, "Advanced languages features");
            this.ShowMenuOptions();

            this._userInterface.ShowMessage(MessageType.Prompt, Messages.EnterTask);
            string? userInput = this._userInterface.GetInput()?.Trim();

            if (string.IsNullOrEmpty(userInput))
            {
                this._userInterface.ShowMessage(MessageType.Warning, Messages.InputCannotBeEmpty);
                Thread.Sleep(1000);
            }
            else if (int.TryParse(userInput, out var userChoice) && userChoice >= 1 && userChoice <= this._tasks.Count)
            {
                this._tasks[userChoice - 1].Run();
            }
            else if (userChoice == this._tasks.Count + 1)
            {
                return;
            }
            else
            {
                this._userInterface.ShowMessage(MessageType.Warning, Messages.EnterValidOption);
                Thread.Sleep(1000);
            }
        }
    }

    /// <summary>
    /// Show the available menu option by iterating <see cref="ITask"s/> with exit option.
    /// </summary>
    private void ShowMenuOptions()
    {
        for (int i = 0; i < this._tasks.Count; i++)
        {
            this._userInterface.ShowMessage(MessageType.Information, string.Format("{0}. {1}", i + 1, this._tasks[i].Name));
        }

        this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.Exit, this._tasks.Count + 1));
    }
}
