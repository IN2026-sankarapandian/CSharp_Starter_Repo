using ErrorHandling.CustomExceptions;
using ErrorHandling.UI;

namespace ErrorHandling.Tasks;

/// <summary>
/// Have methods to list the numbers array and allow user to select any number in it.
/// </summary>
public class Task3
{
    /// <summary>
    /// Entry point to run the task3.
    /// </summary>
    public void Run()
    {
        try
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            ConsoleUI.Display(numbers);
            int selectedIndex = this.GetUserInput("Select a index : ");
            Console.WriteLine($"Selected number at index [{selectedIndex}] is {numbers[selectedIndex]}.");
        }
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

    /// <summary>
    /// Get user input.
    /// </summary>
    /// <param name="prompt">Prompt to show the user.</param>
    /// <returns>User's input.</returns>
    /// <exception cref="InvalidUserInputException">The exception that is thrown when the users input is not valid. </exception>
    public int GetUserInput(string prompt)
    {
        do
        {
            Console.WriteLine(prompt);
            string? selectedIndexString = Console.ReadLine();
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
