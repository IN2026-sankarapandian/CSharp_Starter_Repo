using ValueAndReferenceTypes;

namespace Assignments;

/// <summary>
/// Demonstrates the difference between the reference and value types.
/// </summary>
public class ValueAndReferenceType
{
    /// <summary>
    /// Its an entry point of <see cref="ValueAndReferenceTypes"/> and gets executed first.
    /// Define a value and reference type and pass it to the methods that's changes them.
    /// Print the difference in its behavior.
    /// </summary>
    public static void Main()
    {
        // Task 1
        int integer = 0;
        List<int> integerList = new List<int> { 1, 2, 3, 4, 5 };

        Console.WriteLine("Before function call:\nValue of {0} : {1}\nValue of {2} : {3}", nameof(integer), integer, nameof(integerList), string.Join(",", integerList));

        Change(integer, integerList);

        Console.WriteLine("After function call:\nValue of {0} : {1}\nValue of {2} : {3}", nameof(integer), integer, nameof(integerList), string.Join(",", integerList));

        Console.ReadKey();

        /// This two lines of code is related to task 2 where it creates a instance of <see cref="MemoryAnalysisOfValueAndReferenceType"/>
        /// which demonstrates how memory changes for value and reference types.
        MemoryAnalysisOfValueAndReferenceType memoryAnalysisOfValueAndReferenceType = new MemoryAnalysisOfValueAndReferenceType();
        memoryAnalysisOfValueAndReferenceType.Run();
    }

    /// <summary>
    /// Change the values of the specified value type and one reference type.
    /// </summary>
    /// <param name="integer">Value type.</param>
    /// <param name="integerList">Reference type.</param>
    private static void Change(int integer, List<int> integerList)
    {
        integer = 1;
        integerList.Sort((integerLeft, integerRight) => integerRight.CompareTo(integerLeft));
    }
}