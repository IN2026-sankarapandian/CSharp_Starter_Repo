using System.Numerics;

namespace MemoryOptimization.MemoryEaterOptimized;

/// <summary>
/// This is an optimized version of <see cref="MemoryEater.MemoryEater"/>.
/// </summary>
public class MemoryEater
{
    /// <summary>
    /// It allocates memory to create a integer array.
    /// </summary>
    /// <param name="token">To stop the allocation by canceling.</param>
    /// <param name="maxArrays">Specifies the number of arrays to allocate</param>
    public void Allocate(CancellationToken token, int maxArrays = int.MaxValue)
    {
        List<int[]> memAlloc = new ();
        try
        {
            while (memAlloc.Count < maxArrays)
            {
                memAlloc.Add(new int[10]);
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Cancellation requested, stopped allocation.");
                    break;
                }

                // Assume memAlloc variable is used only within this loop.
                //Thread.Sleep(1000);
            }
        }
        catch (OutOfMemoryException ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
