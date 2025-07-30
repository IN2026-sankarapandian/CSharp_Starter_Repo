using System.Transactions;

namespace ErrorHandling;

/// <summary>
/// This is the task 1.
/// Where we create <see cref="DivideByZeroException"/> voluntarily and catch it.
/// </summary>
public class Task1
{
    /// <summary>
    /// Run the task 1
    /// </summary>
    public void Run()
    {
        int dividend, divisor, result = 0;
        try
        {
            dividend = ConsoleUI.GetNumber("Enter the dividend : ");
            divisor = ConsoleUI.GetNumber("Enter the divisor : ");
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

    private int Divide(int dividend, int divisor)
    {
        return dividend / divisor;
    }
}
