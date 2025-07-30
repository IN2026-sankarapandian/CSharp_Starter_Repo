using ErrorHandling.UI;

namespace ErrorHandling;

/// <summary>
/// Have methods to list the numbers array and allow user to select any number in it.
/// </summary>
public class Task2
{
    /// <summary>
    /// Entry point to run the task2
    /// </summary>
    public void Run()
    {
        int[] numbers = { 1, 2, 3, 4, 5 };

        int selectedNumber = 0;
        try
        {
            selectedNumber = GetValueAtIndex(numbers);
            Console.WriteLine($"Selected number is {numbers[selectedNumber]}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// Gets the value at numbers at user specified index
    /// </summary>
    /// <param name="numbers">Numbers array</param>
    /// <returns>Selected number</returns>
    private static int GetValueAtIndex(int[] numbers)
    {
        try
        {
            ConsoleUI.Display(numbers);
            int selectedIndex = ConsoleUI.GetNumber("Select a index : ");
            return numbers[selectedIndex];
        }
        catch (IndexOutOfRangeException)
        {
            throw new Exception("User attempted to access the element at array with an index that is out of bounds");
        }
    }
}
