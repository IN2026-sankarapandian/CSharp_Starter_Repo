using ErrorHandling.CustomExceptions;

namespace ErrorHandling;

public class Task3
{
    public void Run()
    {
        int[] numbers = { 1, 2, 3, 4, 5 };
        ConsoleUI.Display(numbers);
        try
        {
            int selectedIndex = GetUserInput("Select a index : ");
            Console.WriteLine($"Selected number at index [{selectedIndex}] is {numbers[selectedIndex]}.");
        }1
        catch (InvalidUserInputException ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine(ex.Message);
            if (ex.InnerException is not null)
            {
                Console.WriteLine("Inner Exception : ");
                Console.WriteLine(ex.InnerException);
            }
        }
    }

    private static int GetUserInput(string prompt)
    {
        string? selectedIndexString;
        do
        {
            Console.WriteLine(prompt);
            selectedIndexString = Console.ReadLine();
            if (string.IsNullOrEmpty(selectedIndexString))
            {
                throw new InvalidUserInputException("Input cannot be null", new ArgumentNullException());
            }
            else if (!int.TryParse(selectedIndexString, out int selectedIndex))
            {
                throw new InvalidUserInputException("Input is not a valid number");
            }
            else
            {
                return selectedIndex;
            }
        }
        while (true);
    }
}
