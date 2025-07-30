namespace ErrorHandling.UI;

/// <summary>
/// Provide methods to get input from user and prompt user
/// </summary>
public static class ConsoleUI
{
    /// <summary>
    /// Get user input
    /// </summary>
    /// <param name="prompt">Prompt to be shown to user.</param>
    /// <returns>User's input.</returns>
    public static string GetUserInput(string prompt)
    {
        do
        {
            Console.Write(prompt);
            string? userInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(userInput))
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Input cannot be empty !");
            }
        }
        while (true);
    }

    /// <summary>
    /// Get a input integer from user and return it.
    /// </summary>
    /// <param name="prompt">Prompt to be shown to user.</param>
    /// <returns>User's input.</returns>
    public static int GetNumber(string prompt)
    {
        bool success;
        do
        {
            string numberString = GetUserInput(prompt);
            success = int.TryParse(numberString, out int number);
            if (!success)
            {
                Console.WriteLine("Enter a valid number !");
            }
            else
            {
                return number;
            }
        }
        while (true);
    }

    /// <summary>
    /// Displays the integer array.
    /// </summary>
    /// <param name="numbers">Integer array to display.</param>
    public static void Display(int[] numbers)
    {
        int index = 1;
        foreach (int number in numbers)
        {
            Console.Write($"{nameof(numbers)}[");
            Console.WriteLine($"{index}] = {number}");
            index++;
        }
    }
}
