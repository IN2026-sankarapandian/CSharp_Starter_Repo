using System.Diagnostics;
using System.Text;

namespace Asynchronous.FileServices;

/// <summary>
/// Provide methods to handle files.
/// </summary>
public class FileService
{
    /// <summary>
    /// Asynchronously reads the content in the file,
    /// </summary>
    /// <param name="filePath">Path of the file to read.</param>
    /// <param name="progressCallBack">
    /// An optional callback invoked to report about the progress.
    /// First argument return the percentage of current progress
    /// Second argument returns the elapsed time in seconds since the start of the operation.
    /// </param>
    /// <returns>>A <see cref="Task{string}"/> with read string value representing the asynchronous operation.</returns>
    public async Task<string> ReadFileAsync(string filePath, Action<int, long>? progressCallBack = null)
    {
        using FileStream fileStream = new (
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 1024 * 1024,
            useAsync: true);

        using StreamReader streamReader = new (fileStream);

        long totalBytes = fileStream.Length;
        long readBytes = 0;
        long onePercentBytes = totalBytes / 100;
        long nextThreshold = 0;
        char[] buffer = new char[1024 * 1024];
        int bytesRead;
        StringBuilder stringBuilder = new ();

        Stopwatch stopwatch = new ();
        stopwatch.Start();
        while ((bytesRead = await streamReader.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            stringBuilder.Append(buffer, 0, bytesRead);
            readBytes += bytesRead;

            if (onePercentBytes > 0 && readBytes >= nextThreshold)
            {
                int progress = (int)((readBytes * 100L) / totalBytes);
                progressCallBack?.Invoke(progress, stopwatch.ElapsedMilliseconds);
                nextThreshold += onePercentBytes;
            }
        }

        progressCallBack?.Invoke(100, stopwatch.ElapsedMilliseconds);

        stopwatch.Stop();
        return stringBuilder.ToString();
    }
}
