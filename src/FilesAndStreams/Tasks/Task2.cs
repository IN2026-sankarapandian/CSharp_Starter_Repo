using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using FilesAndStreams.Constants;
using FilesAndStreams.Enums;
using FilesAndStreams.UserInterface;

namespace FilesAndStreams.Tasks;

/// <summary>
/// Demonstrates the how different streams work in asynchronous mode.
/// </summary>
public class Task2
{
    private readonly ConsoleUI _consoleUI;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task2"/> class.
    /// </summary>
    /// <param name="consoleUI">Gives access to console UI</param>
    public Task2(ConsoleUI consoleUI)
    {
        this._consoleUI = consoleUI;
    }

    /// <summary>
    /// Its an entry point for <see cref="Task2"/>
    /// </summary>
    public async void Run()
    {
        string rootPath = AppDomain.CurrentDomain.BaseDirectory;

        string filePath1 = Path.Combine(rootPath, string.Format(FileResources.SampleMachineDataFileName, 1));
        string filePath2 = Path.Combine(rootPath, string.Format(FileResources.SampleMachineDataFileName, 2));

        Console.Clear();
        Console.SetCursorPosition(0, 0);
        this._consoleUI.ShowMessage(MessageType.Title, string.Format(Messages.TaskTitle, 2));
        this._consoleUI.ShowMessage(MessageType.Information, Messages.SampleFileCreationStarted);
        Console.SetCursorPosition(0, 2);
        Task createSampleFile1 = Task.Run(()
            => this.CreateLargeTextFile(filePath1, FileResources.TargetSize, (progress, elapsedTime)
            => this._consoleUI.DrawProgressBar(string.Format(Messages.CreatingSampleFile, 1), progress, 2, elapsedTime)));
        Task createSampleFile2 = Task.Run(()
            => this.CreateLargeTextFile(filePath2, FileResources.TargetSize, (progress, elapsedTime)
            => this._consoleUI.DrawProgressBar(string.Format(Messages.CreatingSampleFile, 2), progress, 3, elapsedTime)));
        createSampleFile1.Wait();
        createSampleFile2.Wait();

        Console.SetCursorPosition(0, 5);
        this._consoleUI.ShowMessage(MessageType.Information, Messages.ReadingSampleFiles);
        Task readFileWithFileStream = this.ReadFileInChunks(FileReader.FileStream, filePath1, (progress, elapsedTime)
            => this._consoleUI.DrawProgressBar(string.Format(Messages.ReadingSampleFileWith, nameof(FileStream)), progress, 6, elapsedTime));
        Task readFileWithBufferedStream = this.ReadFileInChunks(FileReader.BufferedStream, filePath2, (progress, elapsedTime)
            => this._consoleUI.DrawProgressBar(string.Format(Messages.ReadingSampleFileWith, nameof(BufferedStream)), progress, 7, elapsedTime));
        readFileWithFileStream.Wait();
        readFileWithBufferedStream.Wait();

        Console.SetCursorPosition(0, 9);
        this._consoleUI.ShowMessage(MessageType.Information, Messages.FileProcessingStarted);
        Task<string> readFileForProcessing = this.ReadFile(filePath1, (progress, elapsedTime)
            => this._consoleUI.DrawProgressBar(Messages.ReadingSampleFiles, progress, 10, elapsedTime));
        string content = readFileForProcessing.Result;
        string filteredContent = this.FilterByTemperature(content, 100);
        string filteredFilePath = Path.Combine(rootPath, FileResources.SampleFilteredMachineDataFileName);
        this.WriteData(filteredFilePath, filteredContent, (progress, elapsedTime)
            => this._consoleUI.DrawProgressBar(Messages.WritingProcessedData, progress, 11, elapsedTime));
        Console.SetCursorPosition(0, 12);

        this._consoleUI.ShowMessage(MessageType.Information, string.Format(Messages.PressEnterToExitTask, 2));
        Console.ReadKey();
    }

    /// <summary>
    /// Generates a sample machine data files with simulated temperature, pressure and vibration pattern.
    /// </summary>
    /// <param name="filePath">Specify the root path to save the generated file.</param>
    /// <param name="targetSize">Specify the size of the file in bytes.</param>
    /// /// <param name="progressCallBack">
    /// An optional callback invoked to report about the progress.
    /// First argument return the percentage of current progress
    /// Second argument returns the elapsed time in seconds since the start of the operation.
    /// </param>
    private void CreateLargeTextFile(string filePath, decimal targetSize, Action<int, long>? progressCallBack = null)
    {
        File.Create(filePath).Dispose();

        Random random = new ();

        using FileStream writer = new (filePath, FileMode.Create, FileAccess.Write, FileShare.None, FileResources.BufferSize, useAsync: true);
        decimal currentSize = 0;
        int currentLine = 0;
        DateTime startTime = DateTime.Now;

        Stopwatch stopwatch = new ();
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
            writer.WriteAsync(bytes, 0, bytes.Length);
            if (currentSize % (1024 * 1024) == 0)
            {
                int progress = (int)(currentSize / targetSize * 100L);
                progressCallBack?.Invoke(progress, stopwatch.ElapsedMilliseconds);
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
    private async Task ReadFileInChunks(FileReader fileReader, string filePath, Action<int, long>? progressCallBack = null)
    {
        string data = string.Empty;
        if (fileReader == FileReader.FileStream)
        {
            using Stream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None, bufferSize: FileResources.BufferSize, useAsync: true);
            await this.ReadStreamInChunks(fileStream, progressCallBack);
        }
        else if (fileReader == FileReader.BufferedStream)
        {
            using Stream bufferedStream = new BufferedStream(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None, bufferSize: FileResources.BufferSize, useAsync: true));
            await this.ReadStreamInChunks(bufferedStream, progressCallBack);
        }
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
    private async Task ReadStreamInChunks(Stream stream, Action<int, long>? progressCallBack = null, int chunkSize = 500)
    {
        int bytesRead;
        byte[] buffer = new byte[chunkSize];
        decimal totalSize = stream.Length;
        decimal currentSize = 0;

        StringBuilder content = new ();

        Stopwatch stopwatch = new ();
        stopwatch.Start();

        while ((bytesRead = await stream.ReadAsync(buffer.AsMemory(0, chunkSize))) > 1)
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

    /// <summary>
    /// Reads the content in the file,
    /// </summary>
    /// <param name="filePath">Path of the file to read.</param>
    /// <param name="progressCallBack">
    /// An optional callback invoked to report about the progress.
    /// First argument return the percentage of current progress
    /// Second argument returns the elapsed time in seconds since the start of the operation.
    /// </param>
    private async Task<string> ReadFile(string filePath, Action<int, long>? progressCallBack = null)
    {
        using FileStream fileStream =
            new (filePath, FileMode.Open, FileAccess.Read, FileShare.None, bufferSize: FileResources.BufferSize, useAsync: true);
        using StreamReader reader = new (fileStream);
        Stopwatch stopwatch = new ();
        stopwatch.Start();
        progressCallBack?.Invoke(0, stopwatch.ElapsedMilliseconds);
        string res = await reader.ReadToEndAsync();
        progressCallBack?.Invoke(100, stopwatch.ElapsedMilliseconds);
        stopwatch.Stop();
        return res;
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
    private string FilterByTemperature(string content, decimal lowerThreshold, Action<int, long>? progressCallBack = null)
    {
        string[] data = content.Split('\n');
        int totalLine = data.Length;
        int currentLine = 0;
        string pattern = RegexPatterns.ExtractTemperature;
        StringBuilder filteredContent = new ();
        Stopwatch stopwatch = new ();
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
            if (currentLine % 1000 == 0)
            {
                int progress = (int)(currentLine / totalLine * 100L);
                progressCallBack?.Invoke(progress, stopwatch.ElapsedMilliseconds);
            }
        }

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
    private void WriteData(string path, string content, Action<int, long>? progressCallBack = null)
    {
        using MemoryStream memoryStream = new ();
        byte[] bytes = Encoding.UTF8.GetBytes(content);
        memoryStream.Write(bytes, 0, bytes.Length);
        Stopwatch stopwatch = new ();
        stopwatch.Start();
        using (FileStream fileStream =
            new (path, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: FileResources.BufferSize, useAsync: true))
        {
            progressCallBack?.Invoke(0, stopwatch.ElapsedMilliseconds);
            memoryStream.Position = 0;
            memoryStream.CopyToAsync(fileStream);
            progressCallBack?.Invoke(100, stopwatch.ElapsedMilliseconds);
        }

        stopwatch.Stop();
    }
}
