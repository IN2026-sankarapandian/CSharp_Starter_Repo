namespace MemoryOptimization.MemoryEaterOptimized;

/// <summary>
/// This is an optimized version of <see cref="MemoryEater.MemoryEater"/>.
/// </summary>
public class MemoryEater
{
    /// <summary>
    /// It allocates memory to create a integer array.
    /// </summary>
    public void Allocate()
    {
        List<int[]> memAlloc = new ();
        try
        {
            while (true)
            {
                memAlloc.Add(new int[1000]);

                // Assume memAlloc variable is used only within this loop.
                Thread.Sleep(1000);
            }
        }
        catch (OutOfMemoryException ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
