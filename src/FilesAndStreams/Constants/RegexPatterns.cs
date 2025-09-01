namespace FilesAndStreams.Constants;

/// <summary>
/// Have regex validation patterns.
/// </summary>
public static class RegexPatterns
{
    /// <summary>
    /// Its and regex pattern to extract temperature from machine data
    /// </summary>
    public const string ExtractTemperature = @"^(-?\d+)\s*([-+*/])\s*(-?\d+)$";
}
