namespace DisplayApp.Display;

/// <summary>
/// Defines a contract for calculator display.
/// </summary>
public interface ICalculatorDisplay : IUserDisplay
{
    /// <summary>
    /// Display the result for the specified expression.
    /// </summary>
    /// <param name="numberA">First operand.</param>
    /// <param name="numberB">Second operand.</param>
    /// <param name="operatorSymbol">Operation to display result.</param>
    public void DisplayResult(int numberA, int numberB, string operatorSymbol);
}
