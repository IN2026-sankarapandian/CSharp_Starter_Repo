namespace GarbageCollection;

// Task 3

/// <summary>
/// Demonstrates how a garbage collection works and how can it impact the app performance.
/// </summary>
public class GarbageCollection
{
    /// <summary>
    /// Its an entry point of <see cref="GarbageCollection"/> and executed first.
    /// Its calls a method to create and dereference specified large amount of objects and used to debug how memory changes before and after.
    /// </summary>
    public static void Main()
    {
        // Print the initial memory size of heap.
        Console.WriteLine("Initial memory : {0}mb", GC.GetTotalMemory(false) / (1024 * 1024));
        Console.ReadKey();

        // Create large amount of objects and dereference it.
        CreateAndDestroyObjects(500);
        Console.WriteLine("Memory after creating objects : {0}mb", GC.GetTotalMemory(false) / (1024 * 1024));
        Console.ReadKey();

        // Forces GC collect.
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        Console.WriteLine("Memory after GC Collect : {0}mb", GC.GetTotalMemory(false) / (1024 * 1024));

        Console.ReadKey();
    }

    /// <summary>
    /// It creates specified amount of data in mb and dereference it.
    /// </summary>
    /// <param name="size">Amount of data in mb to create and dereference.</param>
    private static void CreateAndDestroyObjects(int size)
    {
        List<byte[]> data = new List<byte[]>();
        for (int count = 0; count < size; count++)
        {
            data.Add(new byte[1024 * 1024]);
        }

        data.Clear();
    }
}