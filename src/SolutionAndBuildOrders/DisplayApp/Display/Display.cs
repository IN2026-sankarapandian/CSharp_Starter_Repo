using DisplayApp.Constants;
using DisplayApp.Enums;
using DisplayApp.Math;

namespace DisplayApp.Displays;

/// <summary>
/// Provides operations to display to user via console.
/// </summary>
public class Display : IDisplay
{
    private ICalculator _calculator;

    /// <summary>
    /// Initializes a new instance of the <see cref="Display"/> class.
    /// </summary>
    /// <param name="calculator">Contains arithmetic calculations methods.</param>
    public Display(ICalculator calculator)
    {
        this._calculator = calculator;
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

        this.ShowMessage(string.Format(Messages.Result, result), MessageType.Result);
    }

    /// <inheritdoc/>
    public void ShowMessage(string message, MessageType type)
    {
        switch (type)
        {
            case MessageType.Prompt:
                Console.Write(message);
                break;
            case MessageType.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(message);
                break;
            case MessageType.Result:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                break;
            case MessageType.Title:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(message);
                break;
        }

        Console.ResetColor();
    }
}