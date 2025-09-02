using DisplayApp.ArithmeticOperations;

namespace MathApp.ArithmeticOperations;

/// <inheritdoc/>
public class ArithmeticOperation : IArithmeticOperations
{
    /// <inheritdoc/>
    public int Add(int a, int b) => a + b;

    /// <inheritdoc/>
    public int Subtract(int a, int b) => a - b;

    /// <inheritdoc/>
    public int Multiply(int a, int b) => a * b;

    /// <inheritdoc/>
    public int Divide(int dividend, int divisor) => dividend / divisor;
}
