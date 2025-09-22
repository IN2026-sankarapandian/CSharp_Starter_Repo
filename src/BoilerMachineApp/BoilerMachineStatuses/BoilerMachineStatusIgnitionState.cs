using BoilerMachineApp.BoilerMachines;
using BoilerMachineApp.Common;

namespace BoilerMachineApp.BoilerMachineStatuses;

/// <summary>
/// It provides the methods by boiler machine in ignition state.
/// </summary>
public class BoilerMachineStatusIgnitionState : IBoilerMachineStatus
{
    private readonly BoilerMachine _boilerMachine;

    /// <summary>
    /// Initializes a new instance of the <see cref="BoilerMachineStatusIgnitionState"/> class.
    /// </summary>
    /// <param name="boilerMachine">Its the main boiler machine.</param>
    public BoilerMachineStatusIgnitionState(BoilerMachine boilerMachine)
    {
        this._boilerMachine = boilerMachine;
        this._boilerMachine.NotifyStateChange("Boiler is in ignition state, it will run for 10 seconds");
        this._boilerMachine.StartTimer(10000);
    }

    /// <inheritdoc/>
    public Result StartBoiling()
    {
        return Result.Failure("Machine is already in operational state (Igniting), can't start boiling again now");
    }

    /// <inheritdoc/>
    public Result StopBoiling()
    {
        this._boilerMachine.StopTimer();
        this._boilerMachine.SetStatus(new BoilerMachineReadyState(this._boilerMachine));
        return Result.Success("Stopped operation at ignition state, moving back to ready state");
    }

    /// <inheritdoc/>
    public Result SimulateBoilerError()
    {
        return Result.Failure("Boiler is in ignition phase, Error can be only simulated when the boiler is in operational mode");
    }

    /// <inheritdoc/>
    public Result ResetLockOut()
    {
        this._boilerMachine.SetStatus(new BoilerMachineLockoutState(this._boilerMachine));
        return Result.Success("Boiler machine reset to lockout state");
    }
}
