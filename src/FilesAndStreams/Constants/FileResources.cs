namespace FilesAndStreams.Constants;

/// <summary>
/// Have constants related to the files.
/// </summary>
public static class FileResources
{
    /// <summary>
    /// Denotes the name of error log file.
    /// </summary>
    public const string ErrorLogFileName = "{0}-ErrorLog.txt";

    /// <summary>
    /// Denotes the date format used to generate sample files.
    /// </summary>
    public const string SampleDateFormat = "yyyy-MM-dd HH:mm:ss";

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

    /// <summary>
    /// Sample error message used in simulating logging errors.
    /// </summary>
    public const string SampleErrorData = "Error A";

    /// <summary>
    /// Sample user id used in simulating logging errors. (User 1)
    /// </summary>
    public const string SampleUserID1 = "001";

    /// <summary>
    /// Sample user id used in simulating logging errors. (User 2)
    /// </summary>
    public const string SampleUserID2 = "002";

    /// <summary>
    /// Sample user id used in simulating logging errors. (User 3)
    /// </summary>
    public const string SampleUserID3 = "003";
}
