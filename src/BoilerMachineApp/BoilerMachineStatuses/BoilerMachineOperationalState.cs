using BoilerMachineApp.BoilerMachines;
using BoilerMachineApp.Common;

namespace BoilerMachineApp.BoilerMachineStatuses;

/// <summary>
/// It provides the methods by boiler machine in operational state.
/// </summary>
public class BoilerMachineOperationalState : IBoilerMachineStatus
{
    private readonly BoilerMachine _boilerMachine;

    /// <summary>
    /// Initializes a new instance of the <see cref="BoilerMachineOperationalState"/> class.
    /// </summary>
    /// <param name="boilerMachine">Its the main boiler machine.</param>
    public BoilerMachineOperationalState(BoilerMachine boilerMachine)
    {
        this._boilerMachine = boilerMachine;
        this._boilerMachine.NotifyStateChange("Boiler is in operational state");
        this._boilerMachine.Logger.Log("Boiler now operational.");
    }

    /// <inheritdoc/>
    public Result StartBoiling()
    {
        return Result.Failure("Machine is already in operational state (Boiling), can't start boiling again now");
    }

    /// <inheritdoc/>
    public Result StopBoiling()
    {
        this._boilerMachine.SetStatus(new BoilerMachineReadyState(this._boilerMachine));
        return Result.Success("Stopped operation, moving back to ready state");
    }

    /// <inheritdoc/>
    public Result SimulateBoilerError()
    {
        this._boilerMachine.SetStatus(new BoilerMachineLockoutState(this._boilerMachine));
        this._boilerMachine.Logger.Log("Error: Simulated error. System in Lockout.");
        return Result.Success("Error simulated successfully, transitioning back to lockout state");
    }

    /// <inheritdoc/>
    public Result ToggleRunInterlockSwitch()
    {
        this._boilerMachine.ToggleInterLock();
        this._boilerMachine.SetStatus(new BoilerMachineLockoutState(this._boilerMachine));
        return Result.Failure("Run interlock is turned open while machine is in operational mode, reset to lockout status");
    }

    /// <inheritdoc/>
    public Result ResetLockOut()
    {
        this._boilerMachine.SetStatus(new BoilerMachineLockoutState(this._boilerMachine));
        return Result.Success("Boiler machine reset to lockout state");
    }
}
