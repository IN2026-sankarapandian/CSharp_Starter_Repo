using ErrorHandling;

namespace Assignments;

internal class Program
{
    public static void Main(string[] args)
    {
        Task1 task1 = new ();
        //task1.Run();

        Task2 task2 = new ();
        task2.Run();

        Task3 task3 = new ();
        //task3.Run();

        Console.ReadKey();
    }
}