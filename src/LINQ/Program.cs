using LINQ.Tasks;
using LINQ.Tasks.Task5;

namespace LINQ;

/// <summary>
/// Its the main class of the project contains main function which is an entry point of the project.
/// </summary>
public class Program
{
    /// <summary>
    /// Its the entry point of a C# application. When the application is started, this method is the first method that is invoked
    /// </summary>
    public static void Main()
    {
        do
        {
            Console.WriteLine("LINQ Assignment");
            Console.WriteLine("\n1. Task 1\n2. Task 2\n3. Task 3\n4. Task 4\n5. Task 5\n6. Exit");
            Console.Write("\nEnter which task to run : ");
            string? userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "1":
                    Task1 task1 = new ();
                    task1.Run();
                    break;
                case "2":
                    Task2 task2 = new ();
                    task2.Run();
                    break;
                case "3":
                    Task3 task3 = new ();
                    task3.Run();
                    break;
                case "4":
                    Task4 task4 = new ();
                    task4.Run();
                    break;
                case "5":
                    Task5 task5 = new ();
                    task5.Run();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }
        }
        while (true);
    }
}