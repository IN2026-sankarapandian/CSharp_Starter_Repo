namespace Asynchronous.Tasks;

/// <summary>
/// Sets the contract for the tasks.
/// </summary>
public interface ITask
{
    /// <summary>
    /// Gets name of the task.
    /// </summary>
    /// <value>
    /// Task name.
    /// </value>
    public string Name { get; }

    /// <summary>
    /// Runs the task.
    /// </summary>
    public void Run();
}
