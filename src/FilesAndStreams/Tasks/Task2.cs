using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FilesAndStreams.Constants;
using FilesAndStreams.Enums;
using FilesAndStreams.FormHandlers;
using FilesAndStreams.UserInterface;

namespace FilesAndStreams.Tasks;

/// <summary>
/// Demonstrates the how different streams work in asynchronous mode.
/// </summary>
public class Task2
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandler _formHandler;
    private readonly HashSet<string> _currentlyProcessingPaths;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task2"/> class.
    /// </summary>
    /// <param name="userInterface">Gives access to UI</param>
    public Task2(IUserInterface userInterface, FormHandler formHandler)
    {
        this._userInterface = userInterface;
        this._formHandler = formHandler;
        this._currentlyProcessingPaths = new HashSet<string>();
    }

    /// <summary>
    /// Its an entry point for <see cref="Task2"/>
    /// </summary>
    public void Run() => this.HandleMenu();

    /// <summary>
    /// Handles the menu for task 1.
    /// </summary>
    private void HandleMenu()
    {
        do
        {
            this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.TaskTitle, 2));
            this._userInterface.ShowMessage(MessageType.Information, "1. Create sample files\n2. Read sample files\n3. Process sample files\n4. Exit");
            string userChoice = this._formHandler.GetUserInput("Enter what do you want to do : ");
            switch (userChoice)
            {
                case "1":
                    this.HandleCreateSampleFilesAsync();
                    break;
                case "2":
                    this.HandleReadSampleFilesAsync();
                    break;
                case "3":
                    this.HandleCreateFilteredFileAsync();
                    break;
                case "4":
                    return;
                default:
                    this._userInterface.ShowMessage(MessageType.Information, "Enter a valid option");
                    break;
            }
        }
        while (true);
    }

    /// <summary>
    /// It will create sample files needed for the task
    /// </summary>
    private async Task HandleCreateSampleFilesAsync()
    {
        this._userInterface.ShowMessage(MessageType.Title, "Create");
        string sampleFileSavePath = this._formHandler.GetTxtFileSavePath("Enter a path to save sample file : ");
        string taskName = string.Format("Writing {0}", Path.GetFileName(sampleFileSavePath));
        this._userInterface.DrawProgressBar(taskName, 0, 0);
        await Task.Run(()
            => this.CreateLargeTextFile(sampleFileSavePath, FileResources.TargetSize, (progress, elapsedTime)
            => this._userInterface.DrawProgressBar(taskName, progress, elapsedTime)));
    }

    /// <summary>
    /// Read the sample files with different streams
    /// </summary>
    private async Task HandleReadSampleFilesAsync()
    {
        this._userInterface.ShowMessage(MessageType.Title, "Read");
        string sampleFileSavePath = this._formHandler.GetTxtFilePath("Enter a path of sample file to read  : ");
        this._userInterface.ShowMessage(MessageType.Information, Messages.ReadingSampleFiles);
        string taskName = string.Format("Reading {0}", Path.GetFileName(sampleFileSavePath));
        this._userInterface.DrawProgressBar(taskName, 0, 0);
        await Task.Run(() => this.ReadFileInChunks(FileReader.FileStream, sampleFileSavePath, (progress, elapsedTime)
            => this._userInterface.DrawProgressBar(taskName, progress, elapsedTime)));
    }

    /// <summary>
    /// Filter the given data by temperature and write the filtered data in new file.
    /// </summary>
    private async void HandleCreateFilteredFileAsync()
    {
        this._userInterface.ShowMessage(MessageType.Title, "Process");
        string sampleFilePath = this._formHandler.GetTxtFilePath("Enter a path of file to filter with temperature : ");
        string filterFilePath = this._formHandler.GetTxtFileSavePath("Enter a path to save sample file : ");
        decimal temperatureThreshold = this._formHandler.GetTemperatureThreshold("Enter a higher threshold temperature to filter : ");
        string readingTaskName = string.Format("Reading {0} for filtering", Path.GetFileName(sampleFilePath));
        string filteringTaskName = string.Format("Filtering {0}", Path.GetFileName(sampleFilePath));
        string writingTaskName = string.Format("Writing filtered data to {0}", Path.GetFileName(filterFilePath));
        this._userInterface.DrawProgressBar(readingTaskName, 0, 0);
        this._userInterface.DrawProgressBar(filteringTaskName, 0, 0);
        this._userInterface.DrawProgressBar(writingTaskName, 0, 0);
        string content = await Task.Run(() => this.ReadFile(sampleFilePath, (progress, elapsedTime)
            => this._userInterface.DrawProgressBar(readingTaskName, progress, elapsedTime)));
        string filteredContent = this.FilterByTemperature(content, 100, (progress, elapsedTime)
            => this._userInterface.DrawProgressBar(filteringTaskName, progress, elapsedTime));
        this.WriteData(filterFilePath, filteredContent, (progress, elapsedTime)
            => this._userInterface.DrawProgressBar(writingTaskName, progress, elapsedTime));
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

        Random random = new();

        using FileStream writer = new (
            filePath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            FileResources.BufferSize,
            useAsync: true);
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
            using Stream fileStream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.None,
                bufferSize: FileResources.BufferSize,
                useAsync: true);
            await this.ReadStreamInChunks(fileStream, progressCallBack);
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

        StringBuilder content = new();

        Stopwatch stopwatch = new();
        stopwatch.Start();

        while ((bytesRead = await stream.ReadAsync(buffer.AsMemory(0, chunkSize))) > 1)
        {
            currentSize += chunkSize;
            if (currentSize % (1024 * 10) == 0)
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
        using FileStream fileStream = new(
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.None,
            bufferSize: FileResources.BufferSize,
            useAsync: true);
        using StreamReader reader = new(fileStream);
        Stopwatch stopwatch = new();
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
        decimal totalLine = data.Length;
        decimal currentLine = 0;
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
            if (currentLine % 1000 == 0)
            {
                int progress = (int)(currentLine / totalLine * 100L);
                progressCallBack?.Invoke(progress, stopwatch.ElapsedMilliseconds);
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
    private void WriteData(string path, string content, Action<int, long>? progressCallBack = null)
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
            memoryStream.CopyToAsync(fileStream);
            progressCallBack?.Invoke(100, stopwatch.ElapsedMilliseconds);
        }

        stopwatch.Stop();
    }
}