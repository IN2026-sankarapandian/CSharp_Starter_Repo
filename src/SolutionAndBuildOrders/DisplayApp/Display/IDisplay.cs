using DisplayApp.Enums;
using DisplayApp.Math;

namespace DisplayApp.Displays;

/// <summary>
/// Defines a contract for user display.
/// </summary>
public interface IDisplay
{
    /// <summary>
    /// Display the result for the specified expression.
    /// </summary>
    /// <param name="numberA">First operand.</param>
    /// <param name="numberB">Second operand.</param>
    /// <param name="operatorSymbol">Operation to display result.</param>
    public void DisplayResult(int numberA, int numberB, string operatorSymbol);

    /// <summary>
    /// Shows the message to user as a specified type.
    /// </summary>
    /// <param name="message">Message shown to user.</param>
    /// <param name="type">Type of the message to show.</param>
    public void ShowMessage(string message, MessageType type);
}