using System.Diagnostics;
using Asynchronous.Constants;
using Asynchronous.Enums;
using Asynchronous.UserInterface;

namespace Asynchronous.Tasks.Task2;

/// <summary>
/// Demonstrates the task 2
/// </summary>
public class Task2 : ITask
{
    private readonly IUserInterface _userInterface;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task2"/> class.
    /// </summary>
    /// <param name="userInterface">Provide method to interact with user via UI</param>
    public Task2(IUserInterface userInterface)
    {
        this._userInterface = userInterface;
    }

    /// <inheritdoc/>
    public string Name => Messages.Task2Title;

    /// <inheritdoc/>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);
        int[] integers = Enumerable.Range(0, 100000000).ToArray();

        Stopwatch stopwatch = Stopwatch.StartNew();
        stopwatch.Start();
        CalculateWithParallel(integers);
        stopwatch.Stop();
        this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.ElapsedTimeParrallel, stopwatch.ElapsedMilliseconds));

        stopwatch.Restart();
        CalculateInSequential(integers);
        stopwatch.Stop();
        this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.ElapsedTimeSequence, stopwatch.ElapsedMilliseconds));

        this._userInterface.ShowMessage(MessageType.Information, Messages.EnterToExit);
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Perform calculation in parallel.
    /// </summary>
    /// <param name="integers">Integers to perform calculation.</param>
    private static void CalculateWithParallel(int[] integers)
    {
        Parallel.ForEach(integers, integer =>
        {
            integer = integer * integer;
        });
    }

    /// <summary>
    /// Perform calculation in sequential.
    /// </summary>
    /// <param name="integers">Integers to perform calculation with.</param>
    private static void CalculateInSequential(int[] integers)
    {
        for (int i = 0; i < integers.Length; i++)
        {
            integers[i] = integers[i] * integers[i];
        }
    }
}
