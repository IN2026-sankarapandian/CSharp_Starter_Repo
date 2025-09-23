namespace Reflections.Tasks.Task6.Calculator;

/// <summary>
/// Set contracts for a calculator with add and subtract method.
/// </summary>
public interface ICalculator
{
    /// <summary>
    /// Adds two integers and returns the result.
    /// </summary>
    /// <param name="a">First integer to add.</param>
    /// <param name="b">Second integer to add.</param>
    /// <returns>The sum of <paramref name="a"> and <paramref name="b">.</returns>
    public int Add(int a, int b);

    /// <summary>
    /// Subtract one integer with another and returns the result.
    /// </summary>
    /// <param name="a">Integer to subtract from.</param>
    /// <param name="b">Integer to subtract with.</param>
    /// <returns>The difference between <paramref name="a"> and <paramref name="b">.</returns>
    public int Subtract(int a, int b);
}
