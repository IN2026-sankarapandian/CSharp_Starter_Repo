using DisplayApp.ArithmeticOperations;
using DisplayApp.Display;
using DisplayApp.Enums;
using MathApp.ArithmeticOperations;

namespace MathApp;

/// <summary>
/// Represents a math app.
/// </summary>
public class Program
{
    /// <summary>
    /// Its an entry point of math app.
    /// </summary>
    public static void Main()
    {
        IUserDisplay userDisplay = new ConsoleDisplay();
        IArithmeticOperations arithmeticOperations = new ArithmeticOperation();
        userDisplay.ShowMessage(MessageType.Prompt, string.Format("Sum of 7 + 7 is {0}", arithmeticOperations.Add(7, 7)));
    }
}