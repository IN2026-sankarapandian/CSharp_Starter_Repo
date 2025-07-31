using ErrorHandling.Tasks;
using ErrorHandling.UI;

namespace ErrorHandling;

/// <summary>
/// It calls the task's class of assignment 8 based on user inputs.
/// </summary>
public class Assignment8
{
    /// <summary>
    /// Its the entry point for assignment 8
    /// </summary>
    public static void Main()
    {
        AppDomain currentDomain = AppDomain.CurrentDomain;
        currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
        do
        {
            Console.WriteLine("1. Task 1\n2. Task 2\n3. Task 3\n4. Task 4\n5. Task 5");
            string userChoice = ConsoleUI.GetUserInput("Enter what to do : ");
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
            }

            Console.ReadKey();
        }
        while (true);
    }

    /// <summary>
    /// Catches the unhandled exceptions globally for the app domain.
    /// </summary>
    /// <param name="sender">Source object that raised the event.</param>
    /// <param name="args">Instance of <see cref="UnhandledExceptionEventHandler"/> class.</param>
    public static void MyHandler(object sender, UnhandledExceptionEventArgs args)
    {
        Exception unhandledException = (Exception)args.ExceptionObject;
        Console.WriteLine($"Unhandled exception caught at App domain function: " + unhandledException);
    }
}