using DisplayApp.ArithmeticOperations;
using DisplayApp.Enums;

namespace DisplayApp.Display;

/// <summary>
/// Represents the calculator display used to display results.
/// </summary>
public class CalculatorDisplay : ConsoleDisplay, ICalculatorDisplay
{
    private readonly IArithmeticOperations _calculator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CalculatorDisplay"/> class.
    /// </summary>
    /// <param name="calculator">Contains arithmetic calculations methods.</param>
    public CalculatorDisplay(IArithmeticOperations calculator)
    {
        this._calculator = calculator;
    }

    /// <summary>
    /// Display the result for the specified expression.
    /// </summary>
    /// <param name="numberA">First operand.</param>
    /// <param name="numberB">Second operand.</param>
    /// <param name="operatorSymbol">Operation to display result.</param>
    public void DisplayResult(int numberA, int numberB, string operatorSymbol)
    {
        if (operatorSymbol.Equals("+"))
        {
            this.ShowMessage(MessageType.Result, this._calculator.Add(numberA, numberB).ToString());
        }
        else if (operatorSymbol.Equals("-"))
        {
            this.ShowMessage(MessageType.Result, this._calculator.Subtract(numberA, numberB).ToString());
        }
        else if (operatorSymbol.Equals("*"))
        {
            this.ShowMessage(MessageType.Result, this._calculator.Multiply(numberA, numberB).ToString());
        }
        else if (operatorSymbol.Equals("/"))
        {
            this.ShowMessage(MessageType.Result, this._calculator.Divide(numberA, numberB).ToString());
        }
    }
}
