namespace IDisposableDemo;

// Task 4

/// <summary>
/// Represents a file writer that can write string to the end of the file.
/// </summary>
public class FileWriter : IDisposable
{
    private StreamWriter _writer;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileWriter"/> class and create a stream writer for an specified path.
    /// </summary>
    /// <remarks>
    /// It also creates a new file if the specified file doesn't exists.</remarks>
    /// <param name="filePath">File path to write.</param>
    public FileWriter(string filePath)
    {
        this._writer = new StreamWriter(filePath, append: true);
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="FileWriter"/> class.
    /// Calls dispose method during finalizer execution.
    /// </summary>
    ~FileWriter()
    {
        this._writer.Dispose();
    }

    /// <summary>
    /// Appends the given string (<paramref name="content"/>) at end of the file.
    /// </summary>
    /// <param name="content">Content to append with the file.</param>
    public void Append(string content) => this._writer.WriteLine(content);

    /// <inheritdoc/>
    public void Dispose()
    {
        this._writer.Dispose();
        GC.SuppressFinalize(this);
    }
}
