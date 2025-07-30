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
        ConsoleUI.Display(numbers);

        int selectedIndex = 0;
        try
        {
            selectedIndex = GetIndex();
            Console.WriteLine($"Selected number at index [{selectedIndex}] is {numbers[selectedIndex]}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine(ex.Message);
        }
    }

    private static int GetIndex()
    {
        try
        {
            int selectedIndex = ConsoleUI.GetNumber("Select a index : ");
            return selectedIndex;
        }
        catch (IndexOutOfRangeException)
        {
            throw new Exception("User attempted to access the element at array with an index that is out of bounds");
        }
    }
}
