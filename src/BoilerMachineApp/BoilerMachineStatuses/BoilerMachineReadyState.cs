using BoilerMachineApp.BoilerMachines;
using BoilerMachineApp.Common;

namespace BoilerMachineApp.BoilerMachineStatuses;

/// <summary>
/// It provides the methods by boiler machine in ready state.
/// </summary>
public class BoilerMachineReadyState : IBoilerMachineStatus
{
    private readonly BoilerMachine _boilerMachine;

    /// <summary>
    /// Initializes a new instance of the <see cref="BoilerMachineReadyState"/> class.
    /// </summary>
    /// <param name="boilerMachine">Its the main boiler machine.</param>
    public BoilerMachineReadyState(BoilerMachine boilerMachine)
    {
        this._boilerMachine = boilerMachine;
        this._boilerMachine.NotifyStateChange("Boiler is ready");
        this._boilerMachine.Logger.Log("Boiler Status changed to Ready");
    }

    /// <inheritdoc/>
    public Result StartBoiling()
    {
        this._boilerMachine.SetStatus(new BoilerMachinePrePurgeState(this._boilerMachine));
        return Result.Success("Started boiling");
    }

    /// <inheritdoc/>
    public Result StopBoiling()
    {
        return Result.Failure("Boiling is not started yet");
    }

    /// <inheritdoc/>
    public Result SimulateBoilerError()
    {
        return Result.Failure("Boiler is not yet started, Error can be only simulated when the boiler is in operational mode");
    }

    /// <inheritdoc/>
    public Result ToggleRunInterlockSwitch()
    {
        this._boilerMachine.ToggleInterLock();
        this._boilerMachine.SetStatus(new BoilerMachineLockoutState(this._boilerMachine));
        return Result.Failure("Run interlock is turned open while machine is ready, reset to lockout status");
    }

    /// <inheritdoc/>
    public Result ResetLockOut()
    {
        this._boilerMachine.SetStatus(new BoilerMachineLockoutState(this._boilerMachine));
        return Result.Success("Boiler machine reset to lockout state");
    }
}
