using DisplayApp.ArithmeticOperations;
using DisplayApp.Display;
using MathApp.ArithmeticOperations;
using UtilityApp.UserInput;

namespace CalculatorApp;

/// <summary>
/// Represents the calculator console app.
/// </summary>
public class Program
{
    /// <summary>
    /// Its an entry point of the calculator console app.
    /// </summary>
    public static void Main()
    {
        IArithmeticOperations arithmeticOperations = new ArithmeticOperation();
        ICalculatorDisplay calculatorDisplay = new CalculatorDisplay(arithmeticOperations);
        IUserInput userInput = new ConsoleInput();

        Calculator calculator = new (calculatorDisplay, userInput);
        calculator.Run();
    }
}