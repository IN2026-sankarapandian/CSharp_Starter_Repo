namespace BoilerMachineApp.Loggers;

/// <summary>
/// Represents the event log data.
/// </summary>
public class EventLog
{
    /// <summary>
    /// Time stamp of the event occurrence
    /// </summary>
    /// <value>Time stamp of the event occurrence.</value>
    public DateTime TimeStamp { get; set; }

    /// <summary>
    /// Gets or sets name of the event.
    /// </summary>
    /// <value> Name of the event.</value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets other data about the event.
    /// </summary>
    /// <value> Other data about the event. </value>
    public string Data { get; set; }
}
