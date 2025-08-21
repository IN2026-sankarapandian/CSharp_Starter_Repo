using DisplayApp.Math;

namespace GreetingsApp;

/// <summary>
/// Greets the user.
/// </summary>
public class App
{
    /// <summary>
    /// Its an entry point of greeting app.
    /// </summary>
    public static void Main()
    {
        Console.WriteLine("Welcome !");
        Calculator calculator = new Calculator();
        calculator.Run();
    }
}