# Memory optimization

## Task 1

*Objective* : To identify and diagnose memory issues in a C# program.

```cs
namespace MemoryOptimization;



public class MemoryEater
{
    private List<int[]> _memAlloc = new List<int[]>();



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
```

*Issue* : The issue is the `memAlloc` object is allocated with the new integer array infinite times which causes out of memory exception.

## Task 2

*Objective* : To fix the memory issue in the provided code snippet and implement memory management best practices.

*Solution* : 
- Here the `memAlloc` is only used within the loop so it can be moved into the function scope.
- The `System.OutOfMemoryException` is not handled so, it may crash the app immediately without any clear message. So in the optimized version I have handled that `System.OutOfMemoryException` exception.


## Task 3
Objective: To understand and demonstrate the use of the memory profiling tool in VS for C#. 

> I have removed the `Thread.Sleep` timer and increased the array allocation size to reach the `outOfMemory` exception sooner as it won't change the actual behavior in memory.
> This screenshots are based on the modified code.

- Before the optimization the process memory was increased until the app crashes with the exception.
 ![screenshot before optimization](Assets/before.png)

- After optimization the object doesn't held by the class instead it was collected by GC after the function ends.
![screenshot before optimization](Assets/after.png)
