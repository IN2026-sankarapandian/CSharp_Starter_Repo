namespace BoilerMachineApp.Loggers;

/// <summary>
/// Its an csv logger saves file into csv.
/// </summary>
public class CSVLogger : ILogger
{
    private readonly string _logFilePath;

    /// <summary>
    /// Initializes a new instance of the <see cref="CSVLogger"/> class.
    /// </summary>
    /// <param name="logFilePath">Path of the log file to save and put log entry.</param>
    public CSVLogger(string logFilePath)
    {
        this._logFilePath = logFilePath;
    }

    /// <inheritdoc/>
    public void Log(string eventName, string? data = default)
    {
        if (!File.Exists(this._logFilePath))
        {
            File.Create(this._logFilePath).Dispose();
        }

        using StreamWriter writer = new (this._logFilePath, append: true);
        string line = $"{DateTime.Now}|{eventName}|{data}";
        writer.WriteLine(line);
    }
}
