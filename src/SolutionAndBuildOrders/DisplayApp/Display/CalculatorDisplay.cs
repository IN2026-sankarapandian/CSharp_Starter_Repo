using ConsoleDisplays.Constants;
using ConsoleDisplays.Displays;
using ConsoleDisplays.Enums;
using DisplayApp.Math;

namespace DisplayApp.Displays;

/// <summary>
/// Provides operations to display the results to user via console.
/// </summary>
public class CalculatorDisplay : ICalculatorDisplay
{
    private ICalculator _calculator;
    private ConsoleDisplay _consoleDisplay;

    /// <summary>
    /// Initializes a new instance of the <see cref="CalculatorDisplay"/> class.
    /// </summary>
    /// <param name="calculator">Contains arithmetic calculations methods.</param>
    public CalculatorDisplay(ICalculator calculator)
    {
        this._calculator = calculator;
        this._consoleDisplay = new ConsoleDisplay();
    }

    /// <inheritdoc/>
    public void DisplayResult(int numberA, int numberB, string operatorSymbol)
    {
        int result = operatorSymbol switch
        {
            "+" => this._calculator.Add(numberA, numberB),
            "-" => this._calculator.Subtract(numberA, numberB),
            "*" => this._calculator.Multiply(numberA, numberB),
            "/" => this._calculator.Divide(numberA, numberB)
        };

        this._consoleDisplay.ShowMessage(string.Format(Messages.Result, result), MessageType.Result);
    }
}