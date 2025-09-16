namespace BoilerMachineApp.Loggers;

/// <summary>
/// Logs events.
/// </summary>
public interface ILogger
{
    /// <summary>
    /// Logs the event
    /// </summary>
    /// <param name="eventName">Name of the event.</param>
    /// <param name="data">Any optional relevant data to the event.</param>
    public void Log(string eventName, string? data = "None");
}
