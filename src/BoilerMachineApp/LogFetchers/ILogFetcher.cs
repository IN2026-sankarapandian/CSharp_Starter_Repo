using BoilerMachineApp.Loggers;

namespace BoilerMachineApp.LogFetchers;

/// <summary>
/// Sets contract for log fetchers.
/// </summary>
public interface ILogFetcher
{
    /// <summary>
    /// Gets the event logs from the specified file.
    /// </summary>
    /// <returns>Returns a list event logs.</returns>
    public List<EventLog> GetEventLogs();
}
