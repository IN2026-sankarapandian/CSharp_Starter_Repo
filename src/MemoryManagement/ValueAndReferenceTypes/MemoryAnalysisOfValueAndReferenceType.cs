namespace ValueAndReferenceTypes;

// Task 2

/// <summary>
/// Demonstrates how value and reference types stores in memory ( heap/ stack frame).
/// </summary>
public class MemoryAnalysisOfValueAndReferenceType
{
    /// <summary>
    /// It calls the method to create reference types and value types one by one to observe how memory was used in runtime.
    /// </summary>
    public void Run()
    {
        this.CreateValueTypes();
        Console.WriteLine("\nPress enter to create value types...");
        Console.ReadKey();
        Console.WriteLine("Value types created.");

        Console.WriteLine("\nPress enter to create reference types...");
        Console.ReadKey();
        this.CreateReferenceTypes();
        Console.WriteLine("Reference types created.");

        Console.WriteLine("\nPress enter to close the app...");
        Console.ReadKey();
    }

    /// <summary>
    /// It create few number of decimals and perform some calculations with those.
    /// </summary>
    private void CreateValueTypes()
    {
        decimal a = 1;
        decimal b = a + 1;
        decimal c = b + 1;
        decimal d = c + 1;
        decimal e = d + 1;
        decimal f = e + 1;
        decimal g = f + 1;
        decimal h = g + 1;
        decimal i = h + 1;
        decimal j = i + 1;
        j--;
    }

    /// <summary>
    /// It creates a large decimal array.
    /// </summary>
    private void CreateReferenceTypes()
    {
        decimal[] decimalArray = new decimal[100000];
        for (int count = 0; count < 100000; count++)
        {
            decimalArray[count] = count++;
        }
    }
}
