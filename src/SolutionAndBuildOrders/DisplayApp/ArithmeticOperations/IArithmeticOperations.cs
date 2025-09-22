namespace DisplayApp.ArithmeticOperations;

/// <summary>
/// Provide methods that do basic arithmetic operations.
/// </summary>
public interface IArithmeticOperations
{
    /// <summary>
    /// Adds two integers and returns the result.
    /// </summary>
    /// <param name="a">First integer to add.</param>
    /// <param name="b">Second integer to add.</param>
    /// <returns>The sum of <paramref name="a"> and <paramref name="b">.</returns>
    int Add(int a, int b);

    /// <summary>
    /// Subtract one integer with another and returns the result.
    /// </summary>
    /// <param name="a">Integer to subtract from.</param>
    /// <param name="b">Integer to subtract with.</param>
    /// <returns>The difference between <paramref name="a"> and <paramref name="b">.</returns>
    int Subtract(int a, int b);

    /// <summary>
    /// Multiplies two integers and returns the result.
    /// </summary>
    /// <param name="a">First integer to add.</param>
    /// <param name="b">Second integer to add.</param>
    /// <returns>The product of <paramref name="a"> and <paramref name="b">.</returns>
    int Multiply(int a, int b);

    /// <summary>
    /// Divides two integers and returns the result.
    /// </summary>
    /// <param name="dividend">Integer to be divided.</param>
    /// <param name="divisor">Integer to divide with.</param>
    /// <returns>The result of <paramref name="dividend"> / <paramref name="divisor"></returns>
    int Divide(int dividend, int divisor);
}
