using ErrorHandling.UI;

namespace ErrorHandling.Tasks;

/// <summary>
/// Same as <see cref="Task3"/> but might throw an <see cref="IndexOutOfRangeException"/> without handling.
/// </summary>
public class Task4
{
    /// <summary>
    /// Entry point to run the task4.
    /// </summary>
    public void Run()
    {
        Task3 task3 = new ();
        int[] numbers = { 1, 2, 3, 4, 5 };
        ConsoleUI.Display(numbers);
        int selectedIndex = task3.GetIndexInput("Select a index : ", numbers);
        Console.WriteLine($"Selected number at index [{selectedIndex}] is {numbers[selectedIndex]}.");
    }
}
