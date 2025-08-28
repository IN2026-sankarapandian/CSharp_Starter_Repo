namespace IDisposableDemo;

// Task 4

/// <summary>
/// Demonstrates the purpose of <see cref="IDisposable"/> and how <see cref="using"/> statement automatically dispose objects and release the files.
/// </summary>
public class IDisposableDemo
{
    /// <summary>
    /// This is an entry point of the <see cref="IDisposableDemo"/>.
    /// </summary>
    /// <remarks>
    /// It will write and read a file with using statement.
    /// </remarks>
    public static void Main()
    {
        string rootPath = AppDomain.CurrentDomain.BaseDirectory;
        string newFilePath = Path.Combine(rootPath, "newfile.txt");
        WriteHelloWorld(newFilePath);
        GC.Collect();
        GC.WaitForPendingFinalizers();

        StreamReader reader = new (newFilePath);
        while (!reader.EndOfStream)
        {
            Console.WriteLine(reader.ReadLine());
        }

        reader.Dispose();
        Console.ReadKey();
    }

    /// <summary>
    /// Write Hello world to the specified file.
    /// </summary>
    /// <param name="filePath">Path of the file to write Hello world.</param>
    private static void WriteHelloWorld(string filePath)
    {
        FileWriter writer = new (filePath);
        writer.Append("Hello world !");
    }
}