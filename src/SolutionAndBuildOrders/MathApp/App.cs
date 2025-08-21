using DisplayApp.Math;

namespace MathApp;

/// <summary>
/// Represents a math app.
/// </summary>
public class App
{
    /// <summary>
    /// Its an entry point of math app.
    /// </summary>
    public static void Main()
    {
        Calculator calculator = new Calculator();
        calculator.Run();
    }
}