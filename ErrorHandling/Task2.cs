using System.Diagnostics;
using System.Security.Cryptography;

namespace ErrorHandling;

public class Task2
{    public void Run()
    {
        int[] numbers = { 1, 2, 3, 4, 5 };
        ConsoleUI.Display(numbers);
        int selectedIndex = 0;
        ConsoleUI.Display(numbers);
        
        try
        {
            selectedIndex = GetIndex();
            Console.WriteLine($"Selected number at index [{selectedIndex}] is {numbers[selectedIndex]}.");23
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine(ex.Message);

        }
    }

    private static int GetIndex()
    {
        int selectedIndex;
        try
        {
            selectedIndex = ConsoleUI.GetNumber("Select a index : ");
        }
        catch (IndexOutOfRangeException)
        {
            throw new Exception("User attempted to access the element at array with an index that is out of bounds");
        }

        return selectedIndex;
    }
}
