using BoilerMachineApp.BoilerMachines;
using BoilerMachineApp.Enums;
using BoilerMachineApp.Loggers;

namespace BoilerMachineApp.UserInterface;

/// <summary>
/// Defines a contract for user interface.
/// </summary>
public interface IUserInterface
{
    /// <summary>
    /// Gets user input and returns it.
    /// </summary>
    /// <returns>User's input</returns>
    public string? GetInput();

    /// <summary>
    /// Shows the message to user as a specified type.
    /// </summary>
    /// <param name="type">Type of the message to show.</param>
    /// <param name="message">Message shown to user.</param>
    public void ShowMessage(MessageType type, string message);

    /// <summary>
    /// Subscribes to the state change event of boiler machine
    /// </summary>
    /// <param name="boilerMachine">Boiler machine to subscribe.</param>
    public void Subscribe(BoilerMachine boilerMachine);

    /// <summary>
    /// Show logs in a separate page
    /// </summary>
    /// <param name="logs">Logs to show</param>
    public void ShowLogs(List<EventLog> logs);
}
