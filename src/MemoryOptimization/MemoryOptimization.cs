namespace MemoryOptimization;

/// <summary>
/// Demonstrates how to diagnose and resolve memory issues.
/// </summary>
public class MemoryOptimization
{
    /// <summary>
    /// This is an entry point of <see cref="MemoryOptimization"/> and runs first when the project is executed.
    /// </summary>
    public static void Main()
    {
        MemoryEaterOptimized.MemoryEater memoryEater = new ();

        CancellationTokenSource cancellationTokenSource = new ();
        Console.WriteLine("Started memory allocation");
        Task.Run(() => memoryEater.Allocate(cancellationTokenSource.Token));

        Console.WriteLine("Press any key to stop allocation");
        Console.ReadKey();
        cancellationTokenSource.Cancel();
        GC.Collect();
        Console.ReadKey();
    }
}