using System.Reflection.PortableExecutable;

namespace FilesAndStreams.Constants;

/// <summary>
/// Have constants related t.
/// </summary>
public static class FileResources
{
    /// <summary>
    /// Denotes the name of sample machine data with index.
    /// </summary>
    public const string SampleMachineDataFileName = "machine - data{0}.txt";

    /// <summary>
    /// Denotes the name of sample filtered machine data.
    /// </summary>
    public const string SampleFilteredMachineDataFileName = "filtered-machine-data.txt";

    /// <summary>
    /// Denotes the name of error log file.
    /// </summary>
    public const string ErrorLogFileName = "{0}-ErrorLog.txt";

    /// <summary>
    /// Denotes the date format used to generate sample files.
    /// </summary>
    public const string SampleDateFormat = "yyyy-MM-dd HH:mm:ss";

    /// <summary>
    /// Denotes the name of the sample files.
    /// </summary>
    public const string SampleFilePath = "sample.text";

    /// <summary>
    /// Denotes the sample data for sample file.
    /// </summary>
    public const string SampleData = "This is Some test Data";

    /// <summary>
    /// Denotes the template to generate sample files.
    /// </summary>
    public const string SampleFileTemplate = "{0}: temperature : {1:F2}°C, pressure : {2:F2} bar, vibration : {3:F2}mm/s";

    /// <summary>
    /// Denotes the target size to generate sample files.
    /// </summary>
    public const long TargetSize = 1024L * 1024L * 512L;

    /// <summary>
    /// Denotes the buffer size used to by all streams across the project.
    /// </summary>
    public const int BufferSize = 4096;
}
