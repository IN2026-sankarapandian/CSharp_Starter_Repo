using BoilerMachineApp.Loggers;

namespace BoilerMachineApp.LogFetchers;

/// <summary>
/// Fetches the event logs from the file.
/// </summary>
public class CsvLogFetcher : ILogFetcher
{
    private readonly string _filePath;

    /// <summary>
    /// Initializes a new instance of the <see cref="CsvLogFetcher"/> class.
    /// </summary>
    /// <param name="filePath">Path of the log file</param>
    public CsvLogFetcher(string filePath)
    {
        this._filePath = filePath;
    }

    /// <inheritdoc/>
    public List<EventLog> GetEventLogs()
    {
        List<EventLog> logs = new List<EventLog>();
        using (StreamReader reader = new StreamReader(this._filePath))
        {
            string content = reader.ReadToEnd();
            string[] lines = content.Split('\n');
            foreach (string line in lines)
            {
                string[] fields = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
                if (fields.Length <= 0)
                {
                    continue;
                }

                EventLog eventLog = new EventLog();

                // Corrupted data will be skipped
                if (!DateTime.TryParse(fields[0], out var timestamp))
                {
                    continue;
                }

                eventLog.TimeStamp = timestamp;
                eventLog.Name = fields[1];
                eventLog.Data = fields[2];
                logs.Add(eventLog);
            }
        }

        return logs;
    }
}
