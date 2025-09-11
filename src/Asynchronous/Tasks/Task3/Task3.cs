using Asynchronous.Constants;
using Asynchronous.Enums;
using Asynchronous.FormHandlers;
using Asynchronous.UserInterface;

namespace Asynchronous.Tasks.Task3;

/// <summary>
/// Demonstrates task 3.
/// </summary>
public class Task3 : ITask
{
    private readonly IUserInterface _userInterface;
    private readonly FormHandler _formHandler;
    private readonly IntegerGenerator _integerGenerator;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task3"/> class.
    /// </summary>
    /// <param name="userInterface">Provide method to interact with user via UI</param>
    /// <param name="formHandler">Provide methods to get data from the user.</param>
    /// <param name="integerGenerator">Provide method to generate integers.</param>
    public Task3(IUserInterface userInterface, FormHandler formHandler, IntegerGenerator integerGenerator)
    {
        this._userInterface = userInterface;
        this._formHandler = formHandler;
        this._integerGenerator = integerGenerator;
    }

    /// <inheritdoc/>
    public string Name => Messages.Task3Title;

    /// <inheritdoc/>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);

        List<int>? primeNumbers = null;
        List<int>? fibonacciSeries = null;

        int primeIntegersRangeMinValue = this._formHandler.GetInteger(Messages.EnterMinValueForPrime);
        int primeIntegersRangeCount = this._formHandler.GetInteger(Messages.EnterCountValueForPrime);

        Thread thread1 = new (() =>
        {
            primeNumbers = this._integerGenerator.GeneratePrimeNumbersRange(primeIntegersRangeMinValue, primeIntegersRangeCount);
        });
        thread1.Start();

        int fibonacciSeriesRangeMinValue = this._formHandler.GetInteger(Messages.EnterMinValueForFib);
        int fibonacciSeriesRangeCount = this._formHandler.GetInteger(Messages.EnterCountValueForFib);
        Thread thread2 = new (() =>
        {
            fibonacciSeries = this._integerGenerator.GenerateFibonacciNumbersRange(fibonacciSeriesRangeMinValue, fibonacciSeriesRangeCount);
        });

        thread2.Start();

        thread1.Join();
        thread2.Join();

        List<int>? result = null;
        if (primeNumbers is not null && fibonacciSeries is not null)
        {
            result = this.CombineResults(primeNumbers, fibonacciSeries);
        }
        else if (primeNumbers is not null)
        {
            result = primeNumbers;
        }
        else if (fibonacciSeries is not null)
        {
            result = fibonacciSeries;
        }

        this.DisplayIntegers(result);

        this._userInterface.ShowMessage(MessageType.Information, Messages.EnterToExit);
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Combine the given arrays and prints it
    /// </summary>
    /// <param name="primeNumbers">Integer array of prime Numbers</param>
    /// <param name="fibonacciSeries">Integer array of fibonacci series.</param>
    private List<int>? CombineResults(List<int> primeNumbers, List<int> fibonacciSeries)
    {
        List<int>? result = primeNumbers.Concat(fibonacciSeries).ToList();
        return result;
    }

    /// <summary>
    /// Displays the integers.
    /// </summary>
    /// <param name="integers">Integers to display.</param>
    private void DisplayIntegers(List<int>? integers)
    {
        if (integers is null)
        {
            this._userInterface.ShowMessage(MessageType.Information, Messages.NoIntegers);
            return;
        }

        this._userInterface.ShowMessage(MessageType.Information, Messages.Result);
        foreach (int integer in integers)
        {
            this._userInterface.ShowMessage(MessageType.Information, $"{integer}, ");
        }

        this._userInterface.ShowMessage(MessageType.Information, string.Empty);
    }
}
