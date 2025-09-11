using Asynchronous.Constants;

namespace Asynchronous.Tasks.Task7;

/// <summary>
/// Have methods that throw exception inside it.
/// </summary>
public class ExceptionThrower
{
    /// <summary>
    /// Throws exception
    /// </summary>
    /// <exception cref="Exception">Test exception</exception>
    public async void VoidAsyncExceptionThrower()
    {
        await Task.Delay(100);
        throw new Exception(Messages.VoidExceptionMessage);
    }

    /// <summary>
    /// Throes exception
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="Exception">Test exception</exception>
    public async Task TaskAsyncExceptionThrower()
    {
        await Task.Delay(100);
        throw new Exception(Messages.TaskExceptionMessage);
    }
}
