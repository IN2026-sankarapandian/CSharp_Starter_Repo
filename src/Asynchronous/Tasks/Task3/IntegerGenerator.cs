namespace Asynchronous.Tasks.Task3;

/// <summary>
/// Provide method to generate integers.
/// </summary>
public class IntegerGenerator
{
    /// <summary>
    /// Generates a sequence of prime number in the given range.
    /// </summary>
    /// <param name="minValue">The minimum value for generating prime numbers with.</param>
    /// <param name="count">The number of sequential prime number to generates.</param>
    /// <returns>A list of prime numbers.</returns>
    public List<int> GeneratePrimeNumbersRange(int minValue, int count)
    {
        List<int> result = new ();
        int currentInteger = minValue;
        while (result.Count <= count)
        {
            if (this.IsPrime(currentInteger))
            {
                result.Add(currentInteger);
            }

            currentInteger++;
        }

        return result;
    }

    /// <summary>
    /// Generates a sequence of fibonacci number in the given range.
    /// </summary>
    /// <param name="minValue">The minimum value for generating fibonacci numbers with.</param>
    /// <param name="count">The number of sequential prime number to generates.</param>
    /// <returns>A list of fibonacci numbers.</returns>
    public List<int> GenerateFibonacciNumbersRange(int minValue, int count)
    {
        List<int> result = new ();
        int next, first = 0, second = 1;
        if (first >= minValue && count >= 1)
        {
            result.Add(first);
        }

        if (second >= minValue && count >= 2)
        {
            result.Add(second);
        }

        while (result.Count <= count)
        {
            next = first + second;
            if (next >= minValue)
            {
                result.Add(next);
            }

            first = second;
            second = next;
        }

        return result;
    }

    /// <summary>
    /// validate whether the specified number is prime umber or not.
    /// </summary>
    /// <param name="number">Number to validate.</param>
    /// <returns>Returns <see cref="true"/> if it is a prime number; otherwise <see cref="false"/></returns>
    private bool IsPrime(int number)
    {
        if (number < 2)
        {
            return false;
        }

        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
            {
                return false;
            }
        }

        return true;
    }
}
