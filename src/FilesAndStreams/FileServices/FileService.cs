using FilesAndStreams.Constants;
using FilesAndStreams.Enums;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace FilesAndStreams.FileServices;

/// <summary>
/// Provide method to handle files.
/// </summary>
public class FileService
{
    /// <summary>
    /// Asynchronously writes a sample machine data files with simulated temperature, pressure and vibration pattern.
    /// </summary>
    /// <param name="filePath">Specify the root path to save the generated file.</param>
    /// <param name="targetSize">Specify the size of the file in bytes.</param>
    /// <param name="progressCallBack">
    /// An optional callback invoked to report about the progress.
    /// First argument return the percentage of current progress
    /// Second argument returns the elapsed time in seconds since the start of the operation.
    /// </param>
    /// <returns>>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task CreateLargeTextFileAsync(string filePath, long targetSize, Action<int, long>? progressCallBack = null)
    {
        File.Create(filePath).Dispose();

        Random random = new();

        using FileStream writer = new(
            filePath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            FileResources.BufferSize,
            useAsync: true);
        long onePercentageSize = targetSize / 100;
        long nextThresoldValue = 0;
        long currentSize = 0;
        int currentLine = 0;
        DateTime startTime = DateTime.Now;

        Stopwatch stopwatch = new();
        stopwatch.Start();
        while (currentSize < targetSize)
        {
            string timeStamp = startTime.AddSeconds(currentLine).ToString(FileResources.SampleDateFormat);
            double temperature = 50 + (random.NextDouble() * 100);
            double pressure = 1 + (random.NextDouble() * 9);
            double vibration = random.NextDouble() * 25;

            string line =
                string.Format(FileResources.SampleFileTemplate + Environment.NewLine, timeStamp, temperature, pressure, vibration);
            byte[] bytes = Encoding.UTF8.GetBytes(line);
            await writer.WriteAsync(bytes, 0, bytes.Length);
            if (onePercentageSize > 0 && currentSize >= nextThresoldValue)
            {
                int progress = (int)((currentSize * 100L) / targetSize);
                progressCallBack?.Invoke(progress, stopwatch.ElapsedMilliseconds);
                nextThresoldValue += onePercentageSize;
            }

            currentSize += bytes.Length;
            currentLine++;
        }

        stopwatch.Stop();
        progressCallBack?.Invoke(100, stopwatch.ElapsedMilliseconds);
    }

    /// <summary>
    /// Asynchronously read the specified file with the specified file writer.
    /// </summary>
    /// <param name="fileReader">Specifies the file writer to use.</param>
    /// <param name="filePath">Path of the file to write.</param>
    /// <param name="progressCallBack">
    /// An optional callback invoked to report about the progress.
    /// First argument return the percentage of current progress
    /// Second argument returns the elapsed time in seconds since the start of the operation.
    /// </param>
    /// <returns>>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task ReadFileInChunksAsync(FileReader fileReader, string filePath, Action<int, long>? progressCallBack = null)
    {
        string data = string.Empty;
        if (fileReader == FileReader.FileStream)
        {
            using Stream fileStream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.None,
                bufferSize: FileResources.BufferSize,
                useAsync: true);
            await this.ReadStreamInChunksAsync(fileStream, progressCallBack);
        }
        else if (fileReader == FileReader.BufferedStream)
        {
            using Stream bufferedStream = new BufferedStream(new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.None,
                bufferSize: FileResources.BufferSize,
                useAsync: true));
            await this.ReadStreamInChunksAsync(bufferedStream, progressCallBack);
        }
    }

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
        using FileStream fileStream = new(
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: FileResources.BufferSize,
            useAsync: true);

        using StreamReader streamReader = new(fileStream);
        Stopwatch stopwatch = new();
        stopwatch.Start();

        long totalBytes = fileStream.Length;
        long readBytes = 0;
        long onePercentBytes = totalBytes / 100;
        long nextThreshold = 0;

        char[] buffer = new char[FileResources.BufferSize];
        int bytesRead;
        StringBuilder sb = new();
        while ((bytesRead = await streamReader.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            sb.Append(buffer, 0, bytesRead);
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
        return sb.ToString();
    }

    /// <summary>
    /// Filters the data with mentioned lower temperature threshold.
    /// </summary>
    /// <param name="content">Data to filter.</param>
    /// <param name="lowerThreshold">Threshold temperature to filter with.</param>
    /// <returns>All the data with temperature higher than lower threshold temperature.</returns>
    /// <param name="progressCallBack">
    /// An optional callback invoked to report about the progress.
    /// First argument return the percentage of current progress
    /// Second argument returns the elapsed time in seconds since the start of the operation.
    /// </param>
    public string FilterByTemperature(string content, decimal lowerThreshold, Action<int, long>? progressCallBack = null)
    {
        string[] data = content.Split('\n');
        long totalLine = data.Length;
        long currentLine = 0;
        long onePercentageOfLines = totalLine / 100;
        long nextThreshold = 0;
        string pattern = RegexPatterns.ExtractTemperature;
        StringBuilder filteredContent = new();
        Stopwatch stopwatch = new();
        stopwatch.Start();
        foreach (string line in data)
        {
            Match match = Regex.Match(line, pattern);
            string temperatureString = match.Groups[1].Value;
            if (decimal.TryParse(temperatureString, out decimal temperature) && temperature > lowerThreshold)
            {
                filteredContent.Append(line + Environment.NewLine);
            }

            currentLine++;
            if (onePercentageOfLines > 0 && currentLine >= nextThreshold)
            {
                int progress = (int)((currentLine * 100L) / totalLine);
                progressCallBack?.Invoke(progress, stopwatch.ElapsedMilliseconds);
                nextThreshold += onePercentageOfLines;
            }
        }

        progressCallBack?.Invoke(100, stopwatch.ElapsedMilliseconds);

        stopwatch.Stop();
        return filteredContent.ToString();
    }

    /// <summary>
    /// Writes the content to the specified path.
    /// </summary>
    /// <param name="path">Path of the file to write.</param>
    /// <param name="content">Content to write in the file.</param>
    /// <param name="progressCallBack">
    /// An optional callback invoked to report about the progress.
    /// First argument return the percentage of current progress
    /// Second argument returns the elapsed time in seconds since the start of the operation.
    /// </param>
    /// <returns>>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task WriteDataAsync(string path, string content, Action<int, long>? progressCallBack = null)
    {
        using MemoryStream memoryStream = new();
        byte[] bytes = Encoding.UTF8.GetBytes(content);
        memoryStream.Write(bytes, 0, bytes.Length);
        Stopwatch stopwatch = new();
        stopwatch.Start();
        using (FileStream fileStream = new(
            path, FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            bufferSize: FileResources.BufferSize,
            useAsync: true))
        {
            progressCallBack?.Invoke(0, stopwatch.ElapsedMilliseconds);
            memoryStream.Position = 0;
            await memoryStream.CopyToAsync(fileStream);
            progressCallBack?.Invoke(100, stopwatch.ElapsedMilliseconds);
        }

        stopwatch.Stop();
    }

    /// <summary>
    /// Generates a sample machine data files with simulated temperature, pressure and vibration pattern.
    /// </summary>
    /// <param name="filePath">Specify the root path to save the generated file.</param>
    /// <param name="targetSize">Specify the size of the file in bytes.</param>
    /// <param name="progressCallBack">
    /// An optional callback invoked to report about the progress.
    /// First argument return the percentage of current progress
    /// Second argument returns the elapsed time in seconds since the start of the operation.
    /// </param>
    public void CreateLargeTextFile(string filePath, long targetSize, Action<int, long>? progressCallBack = null)
    {
        File.Create(filePath).Dispose();

        Random random = new();

        using FileStream writer = new(
            filePath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            FileResources.BufferSize,
            useAsync: false);
        long onePercentageSize = targetSize / 100;
        long nextThresoldValue = 0;
        decimal currentSize = 0;
        int currentLine = 0;
        DateTime startTime = DateTime.Now;

        Stopwatch stopwatch = new();
        stopwatch.Start();
        while (currentSize < targetSize)
        {
            string timeStamp = startTime.AddSeconds(currentLine).ToString(FileResources.SampleDateFormat);
            double temperature = 50 + (random.NextDouble() * 100);
            double pressure = 1 + (random.NextDouble() * 9);
            double vibration = random.NextDouble() * 25;

            string line =
                string.Format(FileResources.SampleFileTemplate + Environment.NewLine, timeStamp, temperature, pressure, vibration);
            byte[] bytes = Encoding.UTF8.GetBytes(line);
            writer.Write(bytes, 0, bytes.Length);
            if (onePercentageSize > 0 && currentSize >= nextThresoldValue)
            {
                int progress = (int)((currentSize * 100L) / targetSize);
                progressCallBack?.Invoke(progress, stopwatch.ElapsedMilliseconds);
                nextThresoldValue += onePercentageSize;
            }

            currentSize += bytes.Length;
            currentLine++;
        }

        stopwatch.Stop();
        progressCallBack?.Invoke(100, stopwatch.ElapsedMilliseconds);
    }

    /// <summary>
    /// Read the specified file with the specified file writer.
    /// </summary>
    /// <param name="fileReader">Specifies the file writer to use.</param>
    /// <param name="filePath">Path of the file to write.</param>
    /// <param name="progressCallBack">
    /// An optional callback invoked to report about the progress.
    /// First argument return the percentage of current progress
    /// Second argument returns the elapsed time in seconds since the start of the operation.
    /// </param>
    public void ReadFileInChunks(FileReader fileReader, string filePath, Action<int, long>? progressCallBack = null)
    {
        string data = string.Empty;
        if (fileReader == FileReader.FileStream)
        {
            using Stream fileStream =
                new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
            this.ReadStreamInChunks(fileStream, progressCallBack);
        }
        else if (fileReader == FileReader.BufferedStream)
        {
            using Stream bufferedStream =
                new BufferedStream(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None));
            this.ReadStreamInChunks(bufferedStream, progressCallBack);
        }
    }

    /// <summary>
    /// Reads the content in the file,
    /// </summary>
    /// <param name="filePath">Path of the file to read.</param>
    /// <param name="progressCallBack">
    /// An optional callback invoked to report about the progress.
    /// First argument return the percentage of current progress
    /// Second argument returns the elapsed time in seconds since the start of the operation.
    /// </param>
    /// <returns>Content read from the file  as string.</returns>
    public string ReadFile(string filePath, Action<int, long>? progressCallBack = null)
    {
        StringBuilder content = new();
        using (FileStream fileStream = new(
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.None))
        {
            long totalSize = fileStream.Length;
            long onePercentBytes = totalSize / 100;
            long nextThreshold = 0;
            using StreamReader reader = new(fileStream);
            Stopwatch stopwatch = new();
            stopwatch.Start();
            string? line;
            progressCallBack?.Invoke(0, stopwatch.ElapsedMilliseconds);
            while ((line = reader.ReadLine()) != null)
            {
                content.AppendLine(line);
                long currentSize = fileStream.Position;
                if (onePercentBytes > 0 && currentSize >= nextThreshold)
                {
                    int progress = (int)((currentSize * 100L) / totalSize);
                    progressCallBack?.Invoke(progress, stopwatch.ElapsedMilliseconds);
                    nextThreshold += onePercentBytes;
                }
            }

            progressCallBack?.Invoke(100, stopwatch.ElapsedMilliseconds);
        }

        return content.ToString();
    }

    /// <summary>
    /// Writes the content to the specified path.
    /// </summary>
    /// <param name="path">Path of the file to write.</param>
    /// <param name="content">Content to write in the file.</param>
    /// <param name="progressCallBack">
    /// An optional callback invoked to report about the progress.
    /// First argument return the percentage of current progress
    /// Second argument returns the elapsed time in seconds since the start of the operation.
    /// </param>
    public void WriteData(string path, string content, Action<int, long>? progressCallBack = null)
    {
        using MemoryStream memoryStream = new();
        byte[] bytes = Encoding.UTF8.GetBytes(content);
        memoryStream.Write(bytes, 0, bytes.Length);
        Stopwatch stopwatch = new();
        stopwatch.Start();
        using (FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            progressCallBack?.Invoke(0, stopwatch.ElapsedMilliseconds);
            memoryStream.Position = 0;
            memoryStream.CopyTo(fileStream);
            progressCallBack?.Invoke(100, stopwatch.ElapsedMilliseconds);
        }

        stopwatch.Stop();
    }
    /// <summary>
    /// Asynchronously read chunk of data with the specified chunk size.
    /// </summary>
    /// <param name="stream">The input stream to read with.</param>
    /// <param name="progressCallBack">
    /// An optional callback invoked to report about the progress.
    /// First argument return the percentage of current progress
    /// Second argument returns the elapsed time in seconds since the start of the operation.
    /// </param>
    /// <param name="chunkSize">The size of each chunk to read.</param>
    private async Task ReadStreamInChunksAsync(Stream stream, Action<int, long>? progressCallBack = null, int chunkSize = 500)
    {
        int bytesRead;
        byte[] buffer = new byte[chunkSize];
        long totalSize = stream.Length;
        long onePercentageSize = totalSize / 100;
        long nextThreshold = onePercentageSize;
        long currentSize = 0;

        StringBuilder content = new();

        Stopwatch stopwatch = new();
        stopwatch.Start();

        while ((bytesRead = await stream.ReadAsync(buffer.AsMemory(0, chunkSize))) > 1)
        {
            currentSize += chunkSize;
            if (onePercentageSize > 0 && currentSize >= nextThreshold)
            {
                int progress = (int)((currentSize * 100L) / totalSize);
                progressCallBack?.Invoke(progress, stopwatch.ElapsedMilliseconds);
                nextThreshold += onePercentageSize;
            }
        }

        stopwatch.Stop();
    }

    /// <summary>
    /// Read chunk of data with the specified chunk size.
    /// </summary>
    /// <param name="stream">The input stream to read with.</param>
    /// <param name="progressCallBack">
    /// An optional callback invoked to report about the progress.
    /// First argument return the percentage of current progress
    /// Second argument returns the elapsed time in seconds since the start of the operation.
    /// </param>
    /// <param name="chunkSize">The size of each chunk to read.</param>
    private void ReadStreamInChunks(Stream stream, Action<int, long>? progressCallBack = null, int chunkSize = 500)
    {
        int bytesRead;
        byte[] buffer = new byte[chunkSize];
        decimal totalSize = stream.Length;
        decimal currentSize = 0;

        StringBuilder content = new();

        Stopwatch stopwatch = new();
        stopwatch.Start();

        while ((bytesRead = stream.Read(buffer, 0, chunkSize)) > 1)
        {
            currentSize += chunkSize;
            if (currentSize % (1024 * 1024) == 0)
            {
                int progress = (int)(currentSize / totalSize * 100L);
                progressCallBack?.Invoke(progress, stopwatch.ElapsedMilliseconds);
            }
        }

        stopwatch.Stop();
        progressCallBack?.Invoke(100, stopwatch.ElapsedMilliseconds);
    }
}
