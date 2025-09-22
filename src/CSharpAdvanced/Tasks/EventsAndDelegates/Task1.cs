using CSharpAdvanced.Constants;
using CSharpAdvanced.Enums;
using CSharpAdvanced.UserInterface;

namespace CSharpAdvanced.Tasks.EventsAndDelegates;

/// <summary>
/// Demonstrates the working of event and delegates.
/// </summary>
public class Task1 : ITask
{
    private readonly Notifier _notifier;
    private readonly IUserInterface _userInterface;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task1"/> class.
    /// </summary>
    /// <param name="notifier">Used to  send notifications.</param>
    /// <param name="userInterface">Provides operations to interact with user.</param>
    public Task1(Notifier notifier, IUserInterface userInterface)
    {
        this._notifier = notifier;
        this._userInterface = userInterface;
    }

    /// <inheritdoc/>
    public string Name => Titles.TaskTitle1;

    /// <inheritdoc/>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);
        this._userInterface.ShowMessage(
            MessageType.Highlight,
            Messages.EventAndDelegateDescription);
        string? userMessage = this.GetUserMessage();

        // Subscribing OnAction with methods that display the details of user input message.
        this._notifier.OnAction += this.DisplayMessage;
        this._notifier.OnAction += this.DisplayMessageInUpperCase;
        this._notifier.OnAction += this.DisplayMessageLength;

        this._notifier.Trigger(userMessage);

        // Unsubscribing after the usage to avoid memory leaks.
        this._notifier.OnAction -= this.DisplayMessage;
        this._notifier.OnAction -= this.DisplayMessageInUpperCase;
        this._notifier.OnAction -= this.DisplayMessageLength;

        this._userInterface.ShowMessage(MessageType.Prompt, string.Format(Messages.TaskExitPrompt, 1));
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Gets the message input from user.
    /// </summary>
    /// <returns>User message.</returns>
    private string GetUserMessage()
    {
        string? userMessage;
        do
        {
            this._userInterface.ShowMessage(MessageType.Prompt, Messages.EnterMessagePrompt);
            userMessage = this._userInterface.GetInput();
            if (string.IsNullOrEmpty(userMessage))
            {
                this._userInterface.ShowMessage(MessageType.Information, Messages.InputCannotBeEmpty);
            }
        }
        while (string.IsNullOrEmpty(userMessage));
        return userMessage;
    }

    /// <summary>
    /// Display the specified message to the user.
    /// </summary>
    /// <param name="message">Message to display to the user.</param>
    private void DisplayMessage(string message)
        => this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.Message, message));

    /// <summary>
    /// Displays the specified message to the user in uppercase.
    /// </summary>
    /// <param name="message">Message to display to the user in uppercase.</param>
    private void DisplayMessageInUpperCase(string message)
        => this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.MessageUppercase, message.ToUpper()));

    /// <summary>
    /// Displays the length of the specified message to the user.
    /// <param name="message">Message to display its length.</param>
    private void DisplayMessageLength(string message)
        => this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.MessageLength, message.Length));
}
