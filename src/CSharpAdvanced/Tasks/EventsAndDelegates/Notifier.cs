using CSharpAdvanced.Constants;
using CSharpAdvanced.Enums;
using CSharpAdvanced.UserInterface;

namespace CSharpAdvanced.Tasks.EventsAndDelegates;

/// <summary>
/// It contain event and methods to send a notification to users.
/// </summary>
public class Notifier
{
    private readonly IUserInterface _userInterface;

    /// <summary>
    /// Initializes a new instance of the <see cref="Notifier"/> class.
    /// </summary>
    /// <param name="userInterface">Provides operations to interact with user.</param>
    public Notifier(IUserInterface userInterface) => this._userInterface = userInterface;

    /// <summary>
    /// Defines a signature for user notification methods.
    /// </summary>
    /// <param name="message">Message to passed to the user in notification.</param>
    public delegate void Notify(string message);

    /// <summary>
    /// Occurs when the <see cref="Notifer"/> triggers.
    /// </summary>
    public event Notify? OnAction;

    /// <summary>
    /// Triggers all the notifier methods subscribed to <see cref="OnAction"/>.
    /// </summary>
    /// <param name="message">Message passed to all the subscribers of <see cref="OnAction"/>.</param>
    public void Trigger(string message)
    {
        this._userInterface.ShowMessage(MessageType.Information, Messages.TriggeringNotifications);
        this.OnAction?.Invoke(message);
    }
}
