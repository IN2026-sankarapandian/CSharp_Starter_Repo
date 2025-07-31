namespace ErrorHandling.Tasks;

/// <summary>
/// Same as <see cref="Task4"/> but it will handle the exceptions on its own.
/// </summary>
public class Task5
{
    /// <summary>
    /// Entry point to run the task5.
    /// </summary>
    public void Run()
    {
        Task4 task4 = new ();
        try
        {
            task4.Run();
        }
        catch (Exception unhandledException)
        {
            Console.WriteLine($"Unhandled exception caught globally : " + unhandledException);
            Console.WriteLine($"Message : " + unhandledException.Message);
            Console.WriteLine($"Stack trace : " + unhandledException?.StackTrace?.ToString());
            Console.WriteLine("Stack trace shows the exception path i.e the origin of the exception");
        }
    }
}
