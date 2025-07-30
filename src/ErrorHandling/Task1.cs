using ErrorHandling.UI;

namespace ErrorHandling;

/// <summary>
/// Have methods to execute divide operation with user inputs.
/// </summary>
public class Task1
{
    /// <summary>
    /// Entry point to run the task1.
    /// </summary>
    public void Run()
    {
        int result = 0;
        try
        {
            int dividend = ConsoleUI.GetNumber("Enter the dividend : ");
            int divisor = ConsoleUI.GetNumber("Enter the divisor : ");
            result = this.Divide(dividend, divisor);
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Divisor was zero setting result as default value 0.");
            result = 0;
        }
        finally
        {
            Console.WriteLine($"Result : {result}");
        }
    }

    /// <summary>
    /// Divide the inputs and return the result
    /// </summary>
    /// <param name="dividend">Dividend</param>
    /// <param name="divisor">Divisor</param>
    /// <returns>Result</returns>
    private int Divide(int dividend, int divisor)
    {
        return dividend / divisor;
    }
}
