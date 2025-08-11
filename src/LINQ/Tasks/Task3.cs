namespace LINQ.Tasks;

/// <summary>
/// Task 3 of the LINQ assignment.
/// </summary>
/// <remarks>LINQ to Objects</remarks>
public class Task3
{
    /// <summary>
    /// Runs the task 3.
    /// </summary>
    public void Run()
    {
        int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7 };
        string numbersString = string.Join(',', numbers);

        int secondLargestNumber = numbers.OrderByDescending(number => number)
                                         .Skip(1).First();

        Console.WriteLine("\n\nSecond largest number in array [{0}] : {1}\n", numbersString, secondLargestNumber);

        int target = 5;
        var pairs = numbers.SelectMany(a => numbers
                           .Where(b => a != b && a + b == target && a < b)
                           .Select(b => new { numberA = a, numberB = b }));

        Console.WriteLine("Unique pairs of numbers in the array [{0}] that add up to a target = {1}", numbersString, target);
        foreach (var pair in pairs)
        {
            Console.WriteLine("{0} - {1}", pair.numberA, pair.numberB);
        }

        Console.WriteLine("Press enter to go back...");
        Console.ReadLine();
        Console.Clear();
    }
}
