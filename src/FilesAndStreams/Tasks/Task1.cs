using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using FilesAndStreams.Constants;
using FilesAndStreams.Enums;
using FilesAndStreams.FormHandlers;
using FilesAndStreams.UserInterface;

namespace FilesAndStreams.Tasks;

/// <summary>
/// Demonstrates the how different streams work in synchronous mode.
/// </summary>
public class Task1
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandler _formHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task1"/> class.
    /// </summary>
    /// <param name="userInterface">Gives access to UI</param>
    /// <param name="formHandler">Get data from user.</param>
    public Task1(IUserInterface userInterface, FormHandler formHandler)
    {
        this._userInterface = userInterface;
        this._formHandler = formHandler;
    }

    /// <summary>
    /// Its an entry point for <see cref="Task1"/>
    /// </summary>
    public void Run() => this.HandleMenu();

    /// <summary>
    /// Handles the menu for task 1.
    /// </summary>
    public void HandleMenu()
    {
        do
        {
            this._userInterface.ShowMessage(MessageType.Title, string.Format(Messages.TaskTitle, 1));
            this._userInterface.ShowMessage(MessageType.Information, "1. Create sample files\n2. Read sample files\n3. Process sample files\n4. Exit");
            string userChoice = this._formHandler.GetUserInput("Enter what do you want to do : ");
            switch (userChoice)
            {
                case "1":
                    this.HandleCreateSampleFiles();
                    break;
                case "2":
                    this.HandleReadSampleFiles();
                    break;
                case "3":
                    this.HandleCreateFilteredFile();
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
    private void HandleCreateSampleFiles()
    {
        this._userInterface.ShowMessage(MessageType.Title, "Create");
        string sampleFileSavePath = this._formHandler.GetTxtFileSavePath("Enter a path to save sample file : ");
        this.CreateLargeTextFile(sampleFileSavePath, FileResources.TargetSize, (progress, elapsedTime)
            => this._userInterface.DrawProgressBar(
                string.Format(Messages.CreatingSampleFile, 1), progress, elapsedTime));
        this._userInterface.ShowMessage(MessageType.Warning, "Press enter to exit");
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Read the sample files with different streams
    /// </summary>
    private void HandleReadSampleFiles()
    {
        this._userInterface.ShowMessage(MessageType.Title, "Read");
        string filePath = this._formHandler.GetTxtFilePath("Enter a path of sample file to read : ");
        this.ReadFileInChunks(FileReader.FileStream, filePath, (progress, elapsedTime)
            => this._userInterface.DrawProgressBar(
                string.Format(Messages.ReadingSampleFileWith, nameof(FileStream)), progress, elapsedTime));
        this.ReadFileInChunks(FileReader.BufferedStream, filePath, (progress, elapsedTime)
            => this._userInterface.DrawProgressBar(
                string.Format(Messages.ReadingSampleFileWith, nameof(BufferedStream)), progress, elapsedTime));
        this._userInterface.ShowMessage(MessageType.Warning, "Press enter to exit");
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Filter the given data by temperature and write the filtered data in new file.
    /// </summary>
    private void HandleCreateFilteredFile()
    {
        this._userInterface.ShowMessage(MessageType.Title, "Process");
        string sampleFilePath = this._formHandler.GetTxtFilePath("Enter a path of file to filter with temperature : ");
        string filterFilePath = this._formHandler.GetTxtFileSavePath("Enter a path to save sample file : ");
        decimal temperatureThreshold = this._formHandler.GetTemperatureThreshold("Enter a higher threshold temperature to filter : ");
        string content = this.ReadFile(sampleFilePath, (progress, elapsedTime)
            => this._userInterface.DrawProgressBar(Messages.ReadingSampleFiles, progress, elapsedTime));
        string filteredContent = this.FilterByTemperature(content, 100, (progress, elapsedTime)
            => this._userInterface.DrawProgressBar("Filtering data", progress, elapsedTime));
        this.WriteData(filterFilePath, filteredContent, (progress, elapsedTime)
            => this._userInterface.DrawProgressBar(Messages.WritingProcessedData, progress, elapsedTime));
        this._userInterface.ShowMessage(MessageType.Warning, "Press enter to exit");
        this._userInterface.GetInput();
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
    private void CreateLargeTextFile(string filePath, decimal targetSize, Action<int, long>? progressCallBack = null)
    {
        File.Create(filePath).Dispose();

        Random random = new ();

        using FileStream writer = new (
            filePath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            FileResources.BufferSize,
            useAsync: false);
        decimal currentSize = 0;
        int currentLine = 0;
        DateTime startTime = DateTime.Now;

        Stopwatch stopwatch = new ();
        stopwatch.Start();
        progressCallBack?.Invoke(0, stopwatch.ElapsedMilliseconds);
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
    private void ReadFileInChunks(FileReader fileReader, string filePath, Action<int, long>? progressCallBack = null)
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

        StringBuilder content = new ();

        Stopwatch stopwatch = new ();
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

    /// <summary>
    /// Reads the content in the file,
    /// </summary>
    /// <param name="filePath">Path of the file to read.</param>
    /// <param name="progressCallBack">
    /// An optional callback invoked to report about the progress.
    /// First argument return the percentage of current progress
    /// Second argument returns the elapsed time in seconds since the start of the operation.
    /// </param>
    private string ReadFile(string filePath, Action<int, long>? progressCallBack = null)
    {
        StringBuilder content = new ();
        using (FileStream fileStream = new (
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.None))
        {
            decimal totalSize = fileStream.Length;
            using StreamReader reader = new (fileStream);
            Stopwatch stopwatch = new ();
            stopwatch.Start();
            string? line;
            progressCallBack?.Invoke(0, stopwatch.ElapsedMilliseconds);
            while ((line = reader.ReadLine()) != null)
            {
                content.AppendLine(line);
                decimal currentSize = fileStream.Position;
                if (currentSize % (1024 * 1024) == 0)
                {
                    int progress = (int)(currentSize / totalSize * 100L);
                    progressCallBack?.Invoke(progress, stopwatch.ElapsedMilliseconds);
                }
            }

            progressCallBack?.Invoke(100, stopwatch.ElapsedMilliseconds);
        }

        return content.ToString();
    }

    /// <summary>
    /// Filters the data with mentioned temperature threshold.
    /// </summary>
    /// <param name="content">Data to filter.</param>
    /// <param name="lowerThreshold">Threshold temperature to filter with.</param>
    /// <returns>All the data with temperature higher than threshold temperature.</returns>
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
        StringBuilder filteredContent = new ();
        Stopwatch stopwatch = new ();
        stopwatch.Start();
        progressCallBack?.Invoke(0, stopwatch.ElapsedMilliseconds);
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
        using MemoryStream memoryStream = new ();
        byte[] bytes = Encoding.UTF8.GetBytes(content);
        memoryStream.Write(bytes, 0, bytes.Length);
        Stopwatch stopwatch = new ();
        stopwatch.Start();
        using (FileStream fileStream = new (path, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            progressCallBack?.Invoke(0, stopwatch.ElapsedMilliseconds);
            memoryStream.Position = 0;
            memoryStream.CopyTo(fileStream);
            progressCallBack?.Invoke(100, stopwatch.ElapsedMilliseconds);
        }

        stopwatch.Stop();
    }
}
