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
        // MemoryEater.MemoryEater memoryEater = new ();
        memoryEater.Allocate();
        GC.Collect();
        Console.ReadKey();
    }
}