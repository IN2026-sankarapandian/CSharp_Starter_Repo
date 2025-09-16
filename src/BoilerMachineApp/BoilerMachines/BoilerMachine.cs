using BoilerMachineApp.BoilerMachineStatuses;
using BoilerMachineApp.Common;
using BoilerMachineApp.Loggers;

namespace BoilerMachineApp.BoilerMachines;

/// <summary>
/// Its an simulated boiler machine.
/// </summary>
public class BoilerMachine
{
    /// <summary>
    /// Current status object of the machine.
    /// </summary>
    private IBoilerMachineStatus _currentStatus;

    /// <summary>
    /// Timer user to simulate operation by the boiler machine.
    /// </summary>
    private System.Timers.Timer _timer;

    /// <summary>
    /// Initializes a new instance of the <see cref="BoilerMachine"/> class.
    /// </summary>
    /// <param name="logger">Logger used to log events.</param>
    public BoilerMachine(ILogger logger)
    {
        this._timer = new System.Timers.Timer();
        this._timer.Elapsed += this.OnTimerElapsed;
        this.Logger = logger;
        this.SetStatus(new BoilerMachineReadyState(this));
    }

    /// <summary>
    /// Its an event triggered when the boiler machine changes it status.
    /// </summary>
    public event Action<string>? OnStateChange;

    /// <summary>
    /// Gets logger used to log events
    /// </summary>
    /// <value>Logger used to log events.</value>
    public ILogger Logger { get; private set; }

    /// <summary>
    /// Starts boiling.
    /// </summary>
    /// <returns>Returns the <see cref="Result"/> object indicating success with success message;
    /// otherwise with an error message indication why the operation failed</returns>
    public Result StartBoiling() => this._currentStatus.StartBoiling();

    /// <summary>
    /// Stops boiling.
    /// </summary>
    /// <returns>Returns the <see cref="Result"/> object indicating success with success message;
    /// otherwise with an error message indication why the operation failed</returns>
    public Result StopBoiling() => this._currentStatus.StopBoiling();

    /// <summary>
    /// Try to simulate some error in boiler operation.
    /// </summary>
    /// <returns>Returns the <see cref="Result"/> object indicating success with success message;
    /// otherwise with an error message indication why the operation failed</returns>
    public Result SimulateBoilerError() => this._currentStatus.SimulateBoilerError();

    /// <summary>
    /// Resets the boiler machine to lockout state manually.
    /// </summary>
    /// <returns>Returns the <see cref="Result"/> object indicating success with success message;
    /// otherwise with an error message indication why the operation failed</returns>
    public Result ResetLockOut() => this._currentStatus.ResetLockOut();

    /// <summary>
    /// Starts the timer with specified interval
    /// </summary>
    /// <param name="interval">Interval to configure the timer</param>
    public void StartTimer(double interval)
    {
        this._timer.Interval = interval;
        this._timer.AutoReset = false;
        this._timer.Start();
    }

    /// <summary>
    /// Stops the timer.
    /// </summary>
    public void StopTimer()
    {
        this._timer.Stop();
    }

    /// <summary>
    /// Sets the status of the boiler machine
    /// </summary>
    /// <param name="status">Status to set with.</param>
    public void SetStatus(IBoilerMachineStatus status)
    {
        this._currentStatus = status;
    }

    /// <summary>
    /// The method is triggered whenever the timer used by machine operation get elapsed.
    /// </summary>
    /// <param name="sender">Instance where the timer elapsed</param>
    /// <param name="e">Event args</param>
    public void OnTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        if (this._currentStatus is BoilerMachineReadyState)
        {
            this.SetStatus(new BoilerMachinePrePurgeState(this));
        }
        else if (this._currentStatus is BoilerMachinePrePurgeState)
        {
            this.Logger.Log("Pre-Purge completed in 10 seconds.");
            this.SetStatus(new BoilerMachineStatusIgnitionState(this));
        }
        else if (this._currentStatus is BoilerMachineStatusIgnitionState)
        {
            this.Logger.Log("Ignition phase completed in 10 seconds.");
            this.SetStatus(new BoilerMachineOperationalState(this));
        }
    }

    /// <summary>
    /// Triggers the state change.
    /// </summary>
    /// <param name="message">Message to be notified for state change.</param>
    public void NotifyStateChange(string message)
    {
        this.OnStateChange?.Invoke(message);
    }
}
