namespace MemoryOptimization.MemoryEater;

/// <summary>
/// This is the sample code snippet with a memory issue.
/// </summary>
public class MemoryEater
{
    private List<int[]> _memAlloc = new ();

    /// <summary>
    /// It allocates memory to create a integer array.
    /// </summary>
    public void Allocate()
    {
        while (true)
        {
            this._memAlloc.Add(new int[1000]);

            // Assume memAlloc variable is used only within this loop.
            Thread.Sleep(1000);
        }
    }
}
