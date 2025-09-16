using BoilerMachineApp.Common;

namespace BoilerMachineApp.BoilerMachineStatuses;

/// <summary>
/// It sets contract for boiler machines state
/// </summary>
public interface IBoilerMachineStatus
{
    /// <summary>
    /// Try to start boiling operation.
    /// </summary>
    /// <returns>Returns the <see cref="Result"/> object indicating success with success message;
    /// otherwise with an error message indication why the operation failed</returns>
    public Result StartBoiling();

    /// <summary>
    /// Try to stop boiling operation
    /// </summary>
    /// <returns>Returns the <see cref="Result"/> object indicating success with success message;
    /// otherwise with an error message indication why the operation failed</returns>
    public Result StopBoiling();

    /// <summary>
    /// Try to simulate some error in boiler operation.
    /// </summary>
    /// <returns>Returns the <see cref="Result"/> object indicating success with success message;
    /// otherwise with an error message indication why the operation failed</returns>
    public Result SimulateBoilerError();

    /// <summary>
    /// Resets the boiler machine to lockout state manually.
    /// </summary>
    /// <returns>Returns the <see cref="Result"/> object indicating success with success message;
    /// otherwise with an error message indication why the operation failed</returns>
    public Result ResetLockOut();
}
