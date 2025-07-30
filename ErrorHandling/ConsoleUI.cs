namespace ErrorHandling;

public static class ConsoleUI
{
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

    public static int GetNumber(string prompt)
    {
        bool success;
        do
        {
            string numberString = ConsoleUI.GetUserInput(prompt);
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

    public static void Display(int[] array)
    {
        int index = 1;
        foreach (int number in array)
        {
            Console.Write("[");
            Console.Write($"{index},{number}");
            Console.WriteLine("]");
            index++;
        }
    }
}
