using BoilerMachineApp.BoilerMachines;
using BoilerMachineApp.Common;

namespace BoilerMachineApp.BoilerMachineStatuses;

/// <summary>
/// It provides the methods by boiler machine in ready state.
/// </summary>
public class BoilerMachineLockoutState : IBoilerMachineStatus
{
    private readonly BoilerMachine _boilerMachine;

    /// <summary>
    /// Initializes a new instance of the <see cref="BoilerMachineLockoutState"/> class.
    /// </summary>
    /// <param name="boilerMachine">Its the main boiler machine.</param>
    public BoilerMachineLockoutState(BoilerMachine boilerMachine)
    {
        this._boilerMachine = boilerMachine;
        this._boilerMachine.NotifyStateChange("Lockout state activated, restart the app for proper functioning");
        this._boilerMachine.Logger.Log("Lockout state activated");
    }

    /// <inheritdoc/>
    public Result StartBoiling()
    {
        this._boilerMachine.StartTimer(1000);
        return Result.Failure("Boiler is at now lockout mode, can't start boiling");
    }

    /// <inheritdoc/>
    public Result StopBoiling()
    {
        return Result.Failure("Boiler is at now lockout mode and boiling is not started yet, can't stop boiling");
    }

    /// <inheritdoc/>
    public Result SimulateBoilerError()
    {
        return Result.Failure("Boiler is at now lockout mode, Error can be only simulated when the boiler is in operational mode");
    }

    /// <inheritdoc/>
    public Result ResetLockOut()
    {
        this._boilerMachine.SetStatus(new BoilerMachineLockoutState(this._boilerMachine));
        return Result.Failure("Boiler machine reset to lockout state, can't reset again");
    }
}
